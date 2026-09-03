
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailSSShop
local shopSystem = GameInstance.player.shopSystem
local SSSHOP_DETAIL_CLIENT_DATA_MANAGER_LAST_SEEN_TIMESTAMP_KEY = "SSShopDetailLastSeenRefresh"
local GRADE_TEXT = {
    Language.LUA_SSSHOP_UNLOCK_ONE,
    Language.LUA_SSSHOP_UNLOCK_TWO,
    Language.LUA_SSSHOP_UNLOCK_THREE,
    Language.LUA_SSSHOP_UNLOCK_FOUR,
}
local GRADE_LOOP = 4
local SEC_PER_DAY = 86400
local SEC_PER_HOUR = 3600
local SEC_PER_MINUTE = 60

MapMarkDetailSSShopCtrl = HL.Class('MapMarkDetailSSShopCtrl', uiCtrl.UICtrl)





MapMarkDetailSSShopCtrl.s_messages = HL.StaticField(HL.Table) << {

}

MapMarkDetailSSShopCtrl.m_shopGroupCellList = HL.Field(HL.Forward("UIListCache"))

MapMarkDetailSSShopCtrl.m_shopGroupList = HL.Field(HL.Any)


MapMarkDetailSSShopCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_shopGroupCellList = UIUtils.genCellCache(self.view.singleShop)

    local markInstId = args.markInstId
    local commonArgs = {
        markInstId = markInstId,
        bigBtnActive = true,
    }

    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)
    local unlockCount = self:_GetUnlockGroupCount()
    local existExceeded = false
    local existLoop = false

    self.m_shopGroupCellList:Refresh(unlockCount, function(singleShop, index)
        local id = self.m_shopGroupList[index].shopGroupId
        local exceeded, loop = self:_FillSingleShop(singleShop, id)
        existExceeded = existExceeded or exceeded
        existLoop = existLoop or loop
    end)


    local _, strLastSeeTimeStamp = ClientDataManagerInst:GetString(SSSHOP_DETAIL_CLIENT_DATA_MANAGER_LAST_SEEN_TIMESTAMP_KEY, false, "0")
    local lastSeeTimeStamp = tonumber(strLastSeeTimeStamp)
    local nextRefreshTimeStamp = Utils.getNextWeeklyServerRefreshTime()
    local thisWeekFirstSee = (nextRefreshTimeStamp - 7 * SEC_PER_DAY) > lastSeeTimeStamp

    self.view.overflowRoot.gameObject:SetActive(existExceeded)
    self.view.refreshingRoot.gameObject:SetActive(existLoop and (not thisWeekFirstSee))
    self.view.refreshCompleteRoot.gameObject:SetActive(existLoop and thisWeekFirstSee)

    if (not existExceeded) and (not existLoop) then
        return
    end

    self:_FillTimeInfo()
end

MapMarkDetailSSShopCtrl._FillTimeInfo = HL.Method() << function(self)
    local nextRefreshTimeStamp = Utils.getNextWeeklyServerRefreshTime()
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local deltaTime = nextRefreshTimeStamp - curTime

    if deltaTime >= SEC_PER_DAY then
        local days = deltaTime // SEC_PER_DAY
        if deltaTime % SEC_PER_DAY > 0 then
            days = days + 1
        end
        self.view.overflowText.text = string.format(Language.LUA_SSSHOP_EXCEEDED_DAY, days)
        self.view.refreshingText.text = string.format(Language.LUA_SSSHOP_REFRESHING_DAY, days)
    elseif deltaTime >= SEC_PER_HOUR then
        local hours = deltaTime // SEC_PER_HOUR
        if deltaTime % SEC_PER_HOUR > 0 then
            hours = hours + 1
        end
        self.view.overflowText.text = string.format(Language.LUA_SSSHOP_EXCEEDED_HOUR, hours)
        self.view.refreshingText.text = string.format(Language.LUA_SSSHOP_REFRESHING_HOUR, hours)
    else
        local minutes = deltaTime // SEC_PER_MINUTE
        if deltaTime % SEC_PER_MINUTE > 0 then
            minutes = minutes + 1
        end
        self.view.overflowText.text = string.format(Language.LUA_SSSHOP_EXCEEDED_MINUTE, minutes)
        self.view.refreshingText.text = string.format(Language.LUA_SSSHOP_REFRESHING_MINUTE, minutes)
    end
end

MapMarkDetailSSShopCtrl._GetUnlockGroupCount = HL.Method().Return(HL.Number) << function(self)
    local list = shopSystem:GetShopListByType(CS.Beyond.GEnums.ShopGroupType.Spaceship, false)
    self.m_shopGroupList = { }

    local ret = 0
    for i = 1, list.Count do
        local data = list[CSIndex(i)]
        local groupId = data.shopGroupId
        local unlocked = shopSystem:CheckShopGroupUnlocked(groupId)
        if unlocked == true then
            table.insert(self.m_shopGroupList, data)
            ret = ret + 1
        end
    end
    return ret
end

MapMarkDetailSSShopCtrl._FillSingleShop = HL.Method(HL.Any, HL.String).Return(HL.Boolean, HL.Boolean) << function(self, singleShop, shopGroupId)

    local shopGroupData = Tables.shopGroupTable[shopGroupId]
    local shopGroupName = shopGroupData.shopGroupName
    singleShop.nameText.text = shopGroupName
    local shopList = shopGroupData.shopIds
    local shopUnlockedCount = 0
    for i = 1, shopList.Count do
        local shopId = shopList[CSIndex(i)]
        if shopSystem:CheckShopUnlocked(shopId) then
            shopUnlockedCount = shopUnlockedCount + 1
        end
    end
    singleShop.unlockCountText.text = GRADE_TEXT[shopUnlockedCount]


    local sampleGoods = Tables.shopTable[shopList[CSIndex(1)]].shopGoodsIds[CSIndex(1)]
    local sampleGoodsInfo = Tables.shopGoodsTable[sampleGoods]
    local moneyId = sampleGoodsInfo.moneyId
    local moneyData = Tables.itemTable[moneyId]
    singleShop.icon:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyData.iconId)

    local moneyCount = Utils.getItemCount(moneyId, true)
    singleShop.pointNumberText.text = tostring(moneyCount)
    local moneyMax = Tables.MoneyConfigTable[moneyId].MoneyClearLimit

    local exceeded = moneyCount > moneyMax
    singleShop.warningRoot.gameObject:SetActive(exceeded)

    return exceeded, shopUnlockedCount >= GRADE_LOOP
end

HL.Commit(MapMarkDetailSSShopCtrl)
