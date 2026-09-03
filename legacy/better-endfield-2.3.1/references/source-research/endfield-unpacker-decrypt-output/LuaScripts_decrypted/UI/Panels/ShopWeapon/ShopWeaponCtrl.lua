
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopWeapon
local PHASE_ID = PhaseId.ShopWeapon
local PERMANENT_BOX_CELL_REFRESH_WAIT_TIME = 0.3
local PERMANENT_BOX_CELL_ROW_REFRESH_WAIT_TIME = 0.05
local PERMANENT_GOODS_CELL_REFRESH_WAIT_TIME = 0.05
ShopWeaponCtrl = HL.Class('ShopWeaponCtrl', uiCtrl.UICtrl)


ShopWeaponCtrl.m_shopSystem = HL.Field(HL.Any)


ShopWeaponCtrl.m_smallUpWeaponCache = HL.Field(HL.Forward("UIListCache"))


ShopWeaponCtrl.m_permanentBoxCell = HL.Field(HL.Forward("UIListCache"))



ShopWeaponCtrl.m_permanentBoxSubBoxCellDict = HL.Field(HL.Table)



ShopWeaponCtrl.m_permanentBoxSubGoodsCellDict = HL.Field(HL.Table)

ShopWeaponCtrl.m_permanentGoodsCell = HL.Field(HL.Forward("UIListCache"))


ShopWeaponCtrl.m_firstNaviData = HL.Field(HL.Any)

ShopWeaponCtrl.m_currNaviData = HL.Field(HL.Any)


ShopWeaponCtrl.m_naviCellTable = HL.Field(HL.Table)

ShopWeaponCtrl.m_currNaviRow = HL.Field(HL.Int) << 1

ShopWeaponCtrl.m_currNaviCol = HL.Field(HL.Int) << 1


ShopWeaponCtrl.m_haveSeenLines = HL.Field(HL.Table)


ShopWeaponCtrl.m_pendingNaviGoodsId = HL.Field(HL.Any)

ShopWeaponCtrl.m_weeklyTimer = HL.Field(HL.Any)



ShopWeaponCtrl.m_pendingAfterTopOrdered = HL.Field(HL.Table)





ShopWeaponCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SHOP_REFRESH] = '_OnShopRefresh',
    [MessageConst.ON_SHOP_JUMP_EVENT] = '_OnShopJumpEvent',
    [MessageConst.ON_SHOP_GOODS_CONDITION_REFRESH] = '_OnShopRefresh',
    [MessageConst.ON_GACHA_POOL_INFO_CHANGED] = 'OnGachaPoolInfoChanged',
    [MessageConst.ON_GACHA_POOL_ROLE_DATA_CHANGED] = 'OnGachaPoolRoleDataChanged',
}


ShopWeaponCtrl.m_normalGoods = HL.Field(HL.Any)





ShopWeaponCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitShortCut()

    self.m_shopSystem = GameInstance.player.shopSystem

    self.m_smallUpWeaponCache = UIUtils.genCellCache(self.view.smallUpWeaponsRoot.shopWeaponBagSmallCell)
    self.m_permanentBoxCell = UIUtils.genCellCache(self.view.commonWeapons.weaponCase)
    self.m_permanentGoodsCell = UIUtils.genCellCache(self.view.commonWeapons.shopWeaponCell)
    self.m_permanentBoxSubBoxCellDict = {}
    self.m_permanentBoxSubGoodsCellDict = {}

    self.m_naviCellTable = {}
    self.m_haveSeenLines = {}
    LuaSystemManager.cashShopItemPrefabSystem:LoadWeaponCaseCell()
    self:UpdateUpWeapon()
    self:UpdateTimeLimitWeapons()
    self:UpdatePermanentWeapon()

    self.view.scroll.onValueChanged:AddListener(function(data)
        self:_ComputeSeeGoods()

        local show = (self.view.scroll.normalizedPosition.y) > 0.05
        if self.view.upWeaponNextPage.gameObject.activeSelf == show then
            return
        end
        if show then
            self.view.upWeaponNextPage.gameObject:SetActive(true)
        else
            self.view.upWeaponNextPage.gameObject:SetActive(false)
        end
    end)


    self:_CustomNaviTarget(self.m_firstNaviData)

    local cashShopCtrl = self.m_phase.cashShopCtrl
    if cashShopCtrl == nil then
        cashShopCtrl = self.m_phase.m_panel2Item[PanelId.CashShop].uiCtrl
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({
        self.view.inputGroup.groupId,
        cashShopCtrl.view.inputGroup.groupId,
    })

    self.m_phase:HidePsStore()

    self:_ProcessArg(arg)
end

ShopWeaponCtrl.OnShow = HL.Override() << function(self)
    Notify(MessageConst.CASH_SHOP_SHOW_WALLET_BAR, {
        moneyIds = {Tables.CashShopConst.WeaponTabMoneyId},
    })

    if self.m_phase.m_needGameEvent then
        self.m_phase.m_needGameEvent = false
        EventLogManagerInst:GameEvent_ShopEnter(
            self.m_phase.m_enterButton,
            self.m_phase.m_enterPanel,
            "",
            CashShopConst.CashShopCategoryType.Weapon,
            ""
        )
    end

    CashShopUtils.TryOpenSpecialGiftPopup()
    CashShopUtils.TryFadeSpecialGiftPopup()
end

ShopWeaponCtrl.OnClose = HL.Override() << function(self)
    local goodsIds = {}
    for _, lineNumber in ipairs(self.m_haveSeenLines) do
        local lineData = self.m_naviCellTable[lineNumber]
        if lineData then
            for _, data in ipairs(lineData) do
                local goodsId = data.goodsId
                if goodsId then
                    table.insert(goodsIds, goodsId)
                end
            end
        end
    end


    if #goodsIds > 0 then
        for _, goodsId in ipairs(goodsIds) do
            if GameInstance.player.shopSystem:IsNewGoodsId(goodsId) then
                GameInstance.player.shopSystem:RecordSeeGoodsId(goodsId)
            end
        end
        GameInstance.player.shopSystem:SetGoodsIdSee()
    end

    if self.m_weeklyTimer and self.m_weeklyTimer > 0 then
        self:_ClearTimer(self.m_weeklyTimer)
    end
end






ShopWeaponCtrl.UpdateUpWeapon = HL.Method() << function(self)
    local _, box, goods = self.m_shopSystem:GetNowUpWeaponData()

    local upBoxList = {}
    local downBoxList = {}

    local tmpBoxList = {}
    local count = box == nil and 0 or box.Count
    for i = 0, count - 1 do

        local goodsData = box[i]
        local goodsId = goodsData.goodsTemplateId
        local _, goodsCfg = Tables.shopGoodsTable:TryGetValue(goodsId);
        local hasGachaCfg, weaponGachaCfg = Tables.gachaWeaponPoolTable:TryGetValue(goodsCfg.weaponGachaPoolId)
        local clientTopTimeId = weaponGachaCfg.clientTopTimeId
        local isTop = Utils.isCurTimeInTimeIdRange(clientTopTimeId)
        logger.info(string.format("[Shop]%s, isTop: %s", goodsId, tostring(isTop)))
        local tmpData = {
            goodsData = goodsData,
            isTop = isTop,
            index = weaponGachaCfg.index,
            sortId = weaponGachaCfg.sortId
        }
        table.insert(tmpBoxList, tmpData)
    end
    table.sort(tmpBoxList, Utils.genSortFunction({ "sortId", "index" }, false))
    for idx, tmpData in ipairs(tmpBoxList) do
        if tmpData.isTop then
            table.insert(upBoxList, tmpData)
        else
            table.insert(downBoxList, tmpData)
        end
    end

    local singleWeaponCase = self.view.randomWeaponsCase
    local doubleWeaponCase = self.view.doubleRandomWeaponsCase
    local upBoxCount = #upBoxList
    local upBoxIsEmpty = upBoxCount == 0

    if upBoxCount == 1 then

        local singleWeaponCaseCell = self:_GetSingleWeaponCell()
        local boxData = upBoxList[1].goodsData
        singleWeaponCase.gameObject:SetActive(true)
        doubleWeaponCase.gameObject:SetActive(false)
        singleWeaponCaseCell.gameObject:SetActive(true)
        singleWeaponCase.stayTunedNode.gameObject:SetActive(false)
        local data = { length = 6,
                       cell = singleWeaponCase.inputBindingGroupNaviDecorator,
                       naviNode = singleWeaponCase.naviNode,
                       goodsId = boxData.goodsId }
        self.m_firstNaviData = data
        table.insert(self.m_naviCellTable, { data })

        local suc = self:_UpdateSingleLimitedWeapon(singleWeaponCaseCell, boxData, false)
        if not suc then
            upBoxIsEmpty = true
            singleWeaponCaseCell.gameObject:SetActive(false)
        else

            local poolId = Tables.shopGoodsTable[boxData.goodsTemplateId].weaponGachaPoolId
            local isRealTime, closeTimeDesc= CashShopUtils.GetGachaWeaponPoolCloseTimeShowDesc(poolId)
            local poolTimeNode = singleWeaponCaseCell.view.poolTimeNode
            poolTimeNode.endTimeTxt.text = closeTimeDesc
            poolTimeNode.endingTxt.gameObject:SetActive(isRealTime)
        end
    elseif upBoxCount >= 2 then
        local boxData1 = upBoxList[1].goodsData
        local boxData2 = upBoxList[2].goodsData

        singleWeaponCase.gameObject:SetActive(false)
        doubleWeaponCase.gameObject:SetActive(true)
        local weaponCaseCell1 = self:_GetDoubleWeaponCell(1)
        local weaponCaseCell2 = self:_GetDoubleWeaponCell(2)
        local data1 = { length = 3, cell = self.view.doubleRandomWeaponsCase.weaponParent1InputBindingGroupNaviDecorator, naviNode = self.view.doubleRandomWeaponsCase.weaponParent1, goodsId = boxData1.goodsId }
        local data2 = { length = 3, cell = self.view.doubleRandomWeaponsCase.weaponParent2InputBindingGroupNaviDecorator, naviNode = self.view.doubleRandomWeaponsCase.weaponParent2, goodsId = boxData2.goodsId }
        self.m_firstNaviData = data1
        table.insert(self.m_naviCellTable, { data1, data2 })

        local suc1 = self:_UpdateSingleLimitedWeapon(weaponCaseCell1, boxData1, true)
        local suc2 = self:_UpdateSingleLimitedWeapon(weaponCaseCell2, boxData2, true)
        if not (suc1 and suc2) then
            upBoxIsEmpty = true
            weaponCaseCell1.gameObject:SetActive(false)
            weaponCaseCell2.gameObject:SetActive(false)
        else

            local poolId1 = Tables.shopGoodsTable[boxData1.goodsTemplateId].weaponGachaPoolId
            local isRealTime1, closeTimeDesc1 = CashShopUtils.GetGachaWeaponPoolCloseTimeShowDesc(poolId1)
            weaponCaseCell1.view.poolTimeNode.endTimeTxt:SetAndResolveTextStyle(closeTimeDesc1)
            weaponCaseCell1.view.poolTimeNode.endingTxt.gameObject:SetActive(isRealTime1)
            local poolId2 = Tables.shopGoodsTable[boxData2.goodsTemplateId].weaponGachaPoolId
            local isRealTime2, closeTimeDesc2 = CashShopUtils.GetGachaWeaponPoolCloseTimeShowDesc(poolId2)
            weaponCaseCell2.view.poolTimeNode.endTimeTxt:SetAndResolveTextStyle(closeTimeDesc2)
            weaponCaseCell2.view.poolTimeNode.endingTxt.gameObject:SetActive(isRealTime2)
        end
    end

    if upBoxIsEmpty then

        self:_ClearTopWeaponCells()
        doubleWeaponCase.gameObject:SetActive(false)
        singleWeaponCase.gameObject:SetActive(true)
        singleWeaponCase.stayTunedNode.gameObject:SetActive(true)
        local data = { length = 6, cell = singleWeaponCase.inputBindingGroupNaviDecorator, naviNode = singleWeaponCase.naviNode }
        self.m_firstNaviData = data
        table.insert(self.m_naviCellTable, { data })
    end

    if #downBoxList == 0 then
        self.view.smallUpWeaponsRoot.gameObject:SetActive(false)
    else
        self.view.smallUpWeaponsRoot.gameObject:SetActive(true)
        local downBoxShowCount = #downBoxList

        local lastIsEmpty = false
        if downBoxShowCount % 2 > 0 then
            downBoxShowCount = downBoxShowCount + 1
            lastIsEmpty = true
        end
        local naviLine = {}
        self.m_smallUpWeaponCache:Refresh(downBoxShowCount, function(cell, index)
            local goodsId = nil
            if index == downBoxShowCount and lastIsEmpty then
                cell.view.activateNode.gameObject:SetActive(false)
                cell.view.nullNode.gameObject:SetActive(true)
                cell.view.redDot.gameObject:SetActive(false)
            else
                cell.view.activateNode.gameObject:SetActive(true)
                cell.view.nullNode.gameObject:SetActive(false)
                cell.view.redDot.gameObject:SetActive(true)
                local goodsData = downBoxList[index].goodsData
                self:_SetupViewSmallUpWeaponCell(cell, goodsData)
                goodsId = goodsData.goodsId
            end

            table.insert(naviLine, {
                length = 3,
                cell = cell.view.inputBindingGroupNaviDecorator,
                goodsId = goodsId
            })
            if index % 2 == 0 then
                table.insert(self.m_naviCellTable, naviLine)
                naviLine = {}
            end
        end)
    end
end

ShopWeaponCtrl._getGachaTimeByGoodsData = HL.Method(HL.Any).Return(HL.Number, HL.Number)
    << function(self, goodsData)
    local goodsCfg = Tables.shopGoodsTable[goodsData.goodsTemplateId]
    local poolId = goodsCfg.weaponGachaPoolId

    local _, poolInfo = GameInstance.player.gacha.poolInfos:TryGetValue(poolId)
    local openTime = poolInfo.openTime
    local closeTime = poolInfo.closeTime
    return openTime, closeTime
end

ShopWeaponCtrl._SetupViewSmallUpWeaponCell = HL.Method(HL.Any, HL.Any) << function(self, cell, goodsData)
    cell:InitCashShopItem(goodsData, true)
    local goodsCfg = Tables.shopGoodsTable[goodsData.goodsTemplateId]
    local poolId = goodsCfg.weaponGachaPoolId
    local weaponPoolCfg = Tables.gachaWeaponPoolTable[poolId]
    local gachaTypeCfg = Tables.gachaWeaponPoolTypeTable[weaponPoolCfg.type]

    local _, poolInfo = GameInstance.player.gacha.poolInfos:TryGetValue(poolId)

    local showHardGuarantee = poolInfo.upGotCount <= 0
    local guaranteeNode = cell.view.guaranteeTextStateLayout
    if showHardGuarantee then
        guaranteeNode.gameObject:SetActive(true)
        guaranteeNode.stateController:SetState("OnlyOne")

        local remainNum = math.ceil((gachaTypeCfg.hardGuarantee - poolInfo.hardGuaranteeProgress) / 10)
        guaranteeNode.guaranteeGetTxt.text = string.format("<color=#FEF000>%d</color> ", remainNum) .. Language.ui_shop_entry_weapon_shop_numbers_1
    else
        guaranteeNode.stateController:SetState("Again")

        local loopRewardInfos = CashShopUtils.GetGachaWeaponLoopRewardInfo(poolId)
        if not loopRewardInfos then
            guaranteeNode.gameObject:SetActive(false)
            return
        else
            guaranteeNode.gameObject:SetActive(true)
            table.sort(loopRewardInfos, function(a, b)
                return a.remainNeedPullCount < b.remainNeedPullCount
            end)

            local info = loopRewardInfos[1]
            local name = info.name
            local number = info.remainNeedPullCount
            guaranteeNode.guaranteeGetTxt.text = string.format(Language.LUA_CASH_SHOP_SMALL_UP_WEAPON_REPEAT_GUARANTEE_TEXT, number, name)
        end
    end

    local isRealTime, closeTimeDesc = CashShopUtils.GetGachaWeaponPoolCloseTimeShowDesc(poolId)
    cell.view.poolTimeNode.endTimeTxt.text = closeTimeDesc
    cell.view.poolTimeNode.endingTxt.gameObject:SetActive(isRealTime)

    cell.view.weaponsBagNameTxt.text = weaponPoolCfg.name

    local icon = weaponPoolCfg.smallPoolIcon
    local iconFar = weaponPoolCfg.smallPoolIconFar
    cell.view.iconWeaponBag:LoadSprite(UIConst.UI_SPRITE_SHOP_WEAPON_BOX, icon)
    if string.isEmpty(iconFar) then
        cell.view.iconWeaponBagFar.gameObject:SetActive(false)
    else
        cell.view.iconWeaponBagFar.gameObject:SetActive(true)
        cell.view.iconWeaponBagFar:LoadSprite(UIConst.UI_SPRITE_SHOP_WEAPON_BOX, iconFar)
    end

    cell.view.redDot:InitRedDot("GachaWeaponCharTenLtTicket", poolId)
end

ShopWeaponCtrl._UpdateSingleLimitedWeapon = HL.Method(HL.Any, HL.Any, HL.Boolean).Return(HL.Boolean) << function(self, weaponCaseCell, boxData, isDoublePool)

    local csGachaSys = GameInstance.player.gacha
    local goodsCfg = Tables.shopGoodsTable[boxData.goodsTemplateId]

    weaponCaseCell:InitCashShopItem(boxData, true)


    local poolId = goodsCfg.weaponGachaPoolId

    local _, poolInfo = csGachaSys.poolInfos:TryGetValue(poolId)
    if poolInfo == nil then
        logger.error("卡池信息不存在！卡池id：" .. poolId)
        return false
    end

    local weaponPoolCfg = Tables.gachaWeaponPoolTable[poolId]
    local gachaTypeCfg = Tables.gachaWeaponPoolTypeTable[weaponPoolCfg.type]
    weaponCaseCell.view.poolNameTxt.text = weaponPoolCfg.name
    weaponCaseCell.view.gachaBtnRedDot:InitRedDot("GachaWeaponCharTenLtTicket", poolId)
    local uiPrefabName = isDoublePool and weaponPoolCfg.doublePoolNodeUIPrefab or weaponPoolCfg.poolNodeUIPrefab
    if weaponCaseCell.view.uiPrefabName ~= uiPrefabName then
        if weaponCaseCell.view.node then
            GameObject.Destroy(weaponCaseCell.view.node)
        end
        local path = string.format("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/CashShop/Widgets/WeaponPoolNode/%s.prefab", uiPrefabName)
        local prefab = self.loader:LoadGameObject(path)
        if not prefab then
            logger.error("武器卡池prefab加载失败！uiPrefabName：", uiPrefabName)
            return false
        end
        local obj = CSUtils.CreateObject(prefab, weaponCaseCell.view.weaponPoolNodeRoot)

        obj.name = weaponPoolCfg.id
        weaponCaseCell.view.uiPrefabName = uiPrefabName
        weaponCaseCell.view.node = obj
    end

    local showHardGuarantee = poolInfo.upGotCount <= 0
    local guaranteeNode = weaponCaseCell.view.guaranteeNode
    if showHardGuarantee then
        local upWeaponId = weaponPoolCfg.upWeaponIds[0]
        local weaponItemCfg = Tables.itemTable[upWeaponId]
        local weaponCfg = Tables.weaponBasicTable[upWeaponId]
        local weaponTypeIconName = UIConst.WEAPON_EXHIBIT_WEAPON_TYPE_ICON_PREFIX .. weaponCfg.weaponType:ToInt()
        guaranteeNode.stateController:SetState("HardGuarantee")
        guaranteeNode.itemIcon:InitItemIcon(upWeaponId)
        guaranteeNode.rewardNameTxt.text = weaponItemCfg.name
        guaranteeNode.weaponTypeIcon:LoadSprite(UIConst.UI_SPRITE_WEAPON_EXHIBIT, weaponTypeIconName)

        local remainNum = math.ceil((gachaTypeCfg.hardGuarantee - poolInfo.hardGuaranteeProgress) / 10)
        guaranteeNode.guaranteeTxt.text = string.format("<color=#FEF000>%d</color> ", remainNum) .. Language.ui_shop_entry_weapon_shop_numbers_1
        guaranteeNode.btn.onClick:RemoveAllListeners()
        guaranteeNode.btn.onClick:AddListener(function()
            CashShopUtils.ShowWikiWeaponPreview(poolId, upWeaponId)
        end)
    else
        guaranteeNode.stateController:SetState("LoopReward")

        local loopRewardInfos = CashShopUtils.GetGachaWeaponLoopRewardInfo(poolId)
        if not loopRewardInfos then
            guaranteeNode.gameObject:SetActive(false)
            return true
        else
            guaranteeNode.gameObject:SetActive(true)
            table.sort(loopRewardInfos, function(a, b)
                return a.remainNeedPullCount < b.remainNeedPullCount
            end)

            local info = loopRewardInfos[1]
            guaranteeNode.itemIcon:InitItemIcon(info.itemId)
            guaranteeNode.rewardNameTxt.text = info.name

            guaranteeNode.guaranteeTxt.text = string.format("%d ", info.remainNeedPullCount) .. Language.ui_shop_entry_weapon_shop_numbers_2
            guaranteeNode.btn.onClick:RemoveAllListeners()
            guaranteeNode.btn.onClick:AddListener(function()

                logger.info("武器卡池，up武器预览或宝箱预览")
                if info.isWeaponItemCase then
                    UIManager:Open(PanelId.BattlePassWeaponCase, { itemId = info.itemId, isPreview = true })
                else
                    CashShopUtils.ShowWikiWeaponPreview(poolId, info.itemId)
                end
            end)
        end
    end
    return true
end




ShopWeaponCtrl.UpdateTimeLimitWeapons = HL.Method() << function(self)
    local now = CS.Beyond.DateTimeUtils.GetCurrentTimestampBySeconds()
    local _, weeklyBox, weeklyGoods = self.m_shopSystem:GetWeeklyWeaponData()
    local _, dailyBox, dailyGoods = self.m_shopSystem:GetDailyWeaponData()
    local weeklyCell = self.view.timeLimitWeapons.weeklyLimitWeapons
    local dailyCell = self.view.timeLimitWeapons.dailyLimitWeapons
    local naviDataLine = { }

    if weeklyGoods ~= nil then

        local weeklyGoodsInfo = {}

        for _, weeklyGood in pairs(weeklyGoods) do
            local goodsTableData = Tables.shopGoodsTable:GetValue(weeklyGood.goodsId)
            local displayItem = UIUtils.getRewardFirstItem(goodsTableData.rewardId)
            local itemId = displayItem.id
            local itemData = Tables.itemTable[itemId]
            table.insert(weeklyGoodsInfo, {
                goodsData = weeklyGood,
                rarity = itemData.rarity
            })
        end
        table.sort(weeklyGoodsInfo, Utils.genSortFunction({"rarity"}, false))
        weeklyCell.shopWeaponCell.gameObject:SetActive(weeklyGoods.Count >= 1)
        if weeklyGoods.Count >= 1 then
            weeklyCell.shopWeaponCell.gameObject:SetActive(true)
            weeklyCell.shopWeaponCell:InitCashShopItem(weeklyGoodsInfo[1].goodsData)
            table.insert(naviDataLine, {
                length = 2,
                cell = weeklyCell.shopWeaponCell.view.inputBindingGroupNaviDecorator,
                goodsId = weeklyGoodsInfo[1].goodsData.goodsId
            })
        end
        weeklyCell.shopWeaponCell02.gameObject:SetActive(weeklyGoods.Count >= 2)
        if weeklyGoods.Count >= 2 then
            weeklyCell.shopWeaponCell02.gameObject:SetActive(true)
            weeklyCell.shopWeaponCell02:InitCashShopItem(weeklyGoodsInfo[2].goodsData)
            table.insert(naviDataLine, {
                length = 1,
                cell = weeklyCell.shopWeaponCell02.view.inputBindingGroupNaviDecorator,
                goodsId = weeklyGoodsInfo[2].goodsData.goodsId
            })
        end

        local weeklyTime = weeklyCell.timeLimitTitleText
        local weeklyEndTime = GameInstance.player.shopSystem:GetWeaponGoodsTimeLimit(weeklyGoods[0]) + 1
        if weeklyEndTime == 0 then
            weeklyEndTime = weeklyGoods[0].closeTimeStamp - DateTimeUtils.GetCurrentTimestampBySeconds() + 1
        end
        weeklyTime.text = string.format(Language.LUA_SHOP_WEAPON_WEEKLY_TIME_LIMIT,
            Utils.appendUTC(Utils.timestampToDateYMDHM(weeklyEndTime + now)))

        if weeklyGoods.Count >= 1 then
            local firstWeeklyGoodsData = weeklyGoodsInfo[1].goodsData
            local closeTime = firstWeeklyGoodsData.closeTimeStamp
            local remainTime = closeTime - DateTimeUtils.GetCurrentTimestampBySeconds()
            logger.info("[ShopWeaponCtrl] 创建周刷新倒计时: " .. remainTime)
            if self.m_weeklyTimer and self.m_weeklyTimer > 0 then
                self:_ClearTimer(self.m_weeklyTimer)
            end
            self.m_weeklyTimer = self:_StartTimer(remainTime, function()
                logger.info("[ShopWeaponCtrl] 因为周刷新倒计时手动发送 MessageConst.ON_SHOP_REFRESH")
                Notify(MessageConst.ON_SHOP_REFRESH)
            end, true)
        end
        if BEYOND_DEBUG then
            local weeklyGoodsNames = {}
            for _, weeklyGood in pairs(weeklyGoods) do
                local goodsTableData = Tables.shopGoodsTable[weeklyGood.goodsTemplateId]
                local displayItem = UIUtils.getRewardFirstItem(goodsTableData.rewardId)
                local itemId = displayItem.id
                local _, itemData = Tables.itemTable:TryGetValue(itemId)
                table.insert(weeklyGoodsNames, itemData.name)
            end
            logger.info("[ShopWeaponCtrl] 周商品名字: " .. table.concat(weeklyGoodsNames, ", "))
        end
    else
        weeklyCell.shopWeaponCell.gameObject:SetActive(false)
        weeklyCell.shopWeaponCell02.gameObject:SetActive(false)
        table.insert(naviDataLine, {
            length = 3,
            cell = weeklyCell.expectLayout.expectLayout,
            goodsId = nil
        })
    end
    weeklyCell.expectLayout.gameObject:SetActive(weeklyGoods == nil)
    weeklyCell.timeLimitNode.gameObject:SetActive(weeklyGoods ~= nil)

    if dailyGoods ~= nil then
        GameInstance.player.shopSystem:SortGoodsList(dailyGoods)
        for i = 1, 3 do
            local goodsCell = dailyCell["shopWeaponCell0" .. i]
            goodsCell.gameObject:SetActive(dailyGoods.Count >= i)
            if dailyGoods.Count >= i then
                goodsCell:InitCashShopItem(dailyGoods[i - 1])
                table.insert(naviDataLine, {
                    length = 1,
                    cell = goodsCell.view.inputBindingGroupNaviDecorator,
                    goodsId = dailyGoods[i-1].goodsId
                })
            end
        end

        local dailyTime = dailyCell.timeLimitTitleText
        local dailyEndTime = GameInstance.player.shopSystem:GetWeaponGoodsTimeLimit(dailyGoods[0]) + 1
        dailyTime.text = string.format(Language.LUA_SHOP_WEAPON_DAILY_TIME_LIMIT,
            Utils.appendUTC(Utils.timestampToDateYMDHM(dailyEndTime + now)))
        if BEYOND_DEBUG then
            local dailyGoodsNames = {}
            for _, dailyGood in pairs(dailyGoods) do
                local goodsTableData = Tables.shopGoodsTable[dailyGood.goodsTemplateId]
                local displayItem = UIUtils.getRewardFirstItem(goodsTableData.rewardId)
                local itemId = displayItem.id
                local _, itemData = Tables.itemTable:TryGetValue(itemId)
                table.insert(dailyGoodsNames, itemData.name)
            end
            logger.info("[ShopWeaponCtrl] 日商品名字: " .. table.concat(dailyGoodsNames, ", "))
        end
    else
        for i = 1, 3 do
            local goodsCell = dailyCell["shopWeaponCell0" .. i]
            goodsCell.gameObject:SetActive(false)
        end
        table.insert(naviDataLine, {
            length = 3,
            cell = dailyCell.expectLayout.expectLayout,
            goodsId = nil
        })
    end
    dailyCell.expectLayout.gameObject:SetActive(dailyGoods == nil)
    dailyCell.timeLimitNode.gameObject:SetActive(dailyGoods ~= nil)

    table.insert(self.m_naviCellTable, naviDataLine)
end


ShopWeaponCtrl.UpdatePermanentWeapon = HL.Method() << function(self)
    local _, box, goods = self.m_shopSystem:GetPermanentWeaponShopData()
    self.m_shopSystem:SortGoodsList(box)
    self.m_shopSystem:SortGoodsList(goods)

    local boxList = self:_GetGoodsDataInfoList(box)
    local goodsList = self:_GetGoodsDataInfoList(goods)


    local rowCount = (#boxList % 3 > 0) and (math.floor(#boxList / 3) + 1) or math.floor(#boxList / 3)

    local useGoodsNumber = (rowCount * 3 - #boxList) * 2
    if useGoodsNumber > #goodsList then
        useGoodsNumber = #goodsList
    end


    local function setupGoodsFunc()
        if #goodsList > useGoodsNumber then
            local naviDataLine = {}
            local countPerLine = self.view.commonWeapons.container.constraintCount
            self.m_permanentGoodsCell:GraduallyRefresh(
                #goodsList - useGoodsNumber,
                PERMANENT_GOODS_CELL_REFRESH_WAIT_TIME,
                function(cell, index)
                    cell.gameObject.name = tostring(index)
                    local trueIndex = index + useGoodsNumber
                    cell:InitCashShopItem(goodsList[trueIndex])
                    table.insert(naviDataLine, {
                        length = 1,
                        cell = cell.view.inputBindingGroupNaviDecorator,
                        goodsId = goodsList[trueIndex].goodsId
                    })
                    if index % countPerLine == 0 or index == #goodsList - useGoodsNumber then

                        table.insert(self.m_naviCellTable, naviDataLine)
                        self:_TrySetNaviFocusByGoodsId(self.m_pendingNaviGoodsId)
                        naviDataLine = {}
                    end
                end)
        else
            self.m_permanentGoodsCell:Refresh(0, function(cell, index)  end)
        end
    end


    local sharedDataList = {}

    self.m_permanentBoxCell:GraduallyRefresh(rowCount, PERMANENT_BOX_CELL_REFRESH_WAIT_TIME, function(cell, index)
        cell.gameObject.name = tostring(index)
        cell.transform:SetSiblingIndex(index - 1)

        self:_SetupPermanentBoxCell(cell, index, rowCount, useGoodsNumber, boxList, goodsList, setupGoodsFunc, sharedDataList)
    end)
end






ShopWeaponCtrl._SetupPermanentBoxCell = HL.Method(HL.Any, HL.Number, HL.Number, HL.Number, HL.Table, HL.Table, HL.Function, HL.Table)
    << function(self, cell, index, rowCount, useGoodsNumber, boxList, goodsList, callback, sharedDataList)



    local lastRowIndex = index - 1
    local lastRowIsFinish = false
    if lastRowIndex == 0 or (sharedDataList[lastRowIndex] and sharedDataList[lastRowIndex].isFinish) then
        lastRowIsFinish = true
    end


    local setupFunc = function()
        local naviDataLine = {}
        local boxCellCache = nil
        local goodsCellCache = nil
        if self.m_permanentBoxSubBoxCellDict[index] then
            boxCellCache = self.m_permanentBoxSubBoxCellDict[index]
        else
            boxCellCache = UIUtils.genCellCache(cell.shopWeaponSuperiorCell)
            self.m_permanentBoxSubBoxCellDict[index] = boxCellCache
        end
        if self.m_permanentBoxSubGoodsCellDict[index] then
            goodsCellCache = self.m_permanentBoxSubGoodsCellDict[index]
        else
            goodsCellCache = UIUtils.genCellCache(cell.shopWeaponCell)
            self.m_permanentBoxSubGoodsCellDict[index] = goodsCellCache
        end

        local setupGoodsFunc = function()
            local rowEndCallback = function()
                table.insert(self.m_naviCellTable, naviDataLine)
                self:_TrySetNaviFocusByGoodsId(self.m_pendingNaviGoodsId)


                local nextRowIndex = index + 1
                if sharedDataList[nextRowIndex] and sharedDataList[nextRowIndex].setupFunc then
                    sharedDataList[nextRowIndex].setupFunc()
                end


                if index == rowCount then
                    callback()
                end
            end

            if index == rowCount and useGoodsNumber > 0 then
                goodsCellCache:GraduallyRefresh(
                    useGoodsNumber,
                    PERMANENT_BOX_CELL_ROW_REFRESH_WAIT_TIME,
                    function(goodsCell, goodsIndex)
                        goodsCell.gameObject.name = "goodsCell" .. tostring(goodsIndex)
                        goodsCell:InitCashShopItem(goodsList[goodsIndex])
                        table.insert(naviDataLine, {
                            length = 1,
                            cell = goodsCell.view.inputBindingGroupNaviDecorator,
                            goodsId = goodsList[goodsIndex].goodsId })

                        if goodsIndex == useGoodsNumber then
                            rowEndCallback()
                        end
                    end
                )
            else
                goodsCellCache:Refresh(0, nil)
                rowEndCallback()
            end
        end

        local startBoxIndex = 3 * (index - 1) + 1
        local endBoxIndex = (3 * index <= #boxList) and (3 * index) or #boxList
        boxCellCache:GraduallyRefresh(
            endBoxIndex - startBoxIndex + 1,
            PERMANENT_BOX_CELL_ROW_REFRESH_WAIT_TIME,
            function(boxCell, boxIndex)
                boxCell.gameObject.name = "boxCell" .. tostring(boxIndex)
                local trueIndex = startBoxIndex + boxIndex - 1
                boxCell:InitCashShopItem(boxList[trueIndex])
                boxCell.view.cashShopItemTag.gameObject:SetActive(false)
                table.insert(naviDataLine, {
                    length = 2,
                    cell = boxCell.view.inputBindingGroupNaviDecorator,
                    goodsId = boxList[trueIndex].goodsId })


                if boxIndex == endBoxIndex - startBoxIndex + 1 then
                    setupGoodsFunc()
                end
            end
        )
    end

    if lastRowIsFinish then
        setupFunc()
        sharedDataList[index] = {
            isFinish = true,
            setupFunc = nil,
        }
    else
        sharedDataList[index] = {
            isFinish = false,
            setupFunc = setupFunc
        }
    end
end





ShopWeaponCtrl._OnShopRefresh = HL.Method() << function(self)
    local isOpen, shopDetailCtrl = UIManager:IsOpen(PanelId.ShopDetail)
    if isOpen then
        shopDetailCtrl:TryClose()
    end

    self:UpdateAll()
end

ShopWeaponCtrl._OnShopJumpEvent = HL.Method() << function(self)
    self:OnClickGoods()
end

ShopWeaponCtrl.OnGachaPoolInfoChanged = HL.Method() << function(self)
    self:UpdateUpWeapon()
    self:UpdateTimeLimitWeapons()
end

ShopWeaponCtrl.OnGachaPoolRoleDataChanged = HL.Method() << function(self)
    self:UpdateUpWeapon()
    self:UpdateTimeLimitWeapons()
end





ShopWeaponCtrl.ChooseLimitedWeaponPool = HL.Method(HL.Any) << function(self, boxData)
    PhaseManager:OpenPhase(PhaseId.GachaWeaponPool, { goodsData = boxData })
end

ShopWeaponCtrl.UpdateAll = HL.Method() << function(self)
    self.m_naviCellTable = {}
    self.m_pendingNaviGoodsId = nil

    self:UpdateUpWeapon()
    self:UpdateTimeLimitWeapons()
    self:UpdatePermanentWeapon()


    self.m_currNaviRow = math.min(self.m_currNaviRow, #self.m_naviCellTable)
    local currRow = self.m_naviCellTable[self.m_currNaviRow]
    self.m_currNaviCol = math.min(self.m_currNaviCol, #currRow)
    local data = currRow[self.m_currNaviCol]
    self:_CustomNaviTarget(data)
end

ShopWeaponCtrl.OnClickGoods = HL.Method(HL.Opt(HL.Table)) << function(self, arg)
    local goodsId = arg.goods
    if goodsId then
        local goods = Tables.shopGoodsTable[goodsId]
        local shopId = goods.shopId
        local goodsData = self.m_shopSystem:GetShopGoodsData(shopId, goodsId)
        if goodsData == nil then
            logger.error(ELogChannel.UI, "商店商品数据为空")
            return
        end

        local isBox = string.isEmpty(goods.rewardId)
        if isBox then
            PhaseManager:OpenPhase(PhaseId.GachaWeaponPool, {goodsData = goodsData})
        else
            UIManager:Open(PanelId.ShopDetail, goodsData)
        end
        return
    end

    if arg.sourceId and arg.targetId then
        PhaseManager:OpenPhase(PhaseId.CommonMoneyExchange, {sourceId = arg.sourceId, targetId = arg.targetId})
    end
end

ShopWeaponCtrl.SetCashShopStateArg = HL.Method(HL.Table) << function(self, arg)

    local isOpen, shopDetailCtrl = UIManager:IsOpen(PanelId.ShopDetail)
    if isOpen then
        local info = shopDetailCtrl:GetInfo()
        arg.goodsId = info.goodsTemplateId
    end
end



ShopWeaponCtrl.OnAfterCategoryTopOrdered = HL.Method() << function(self)
    if not self.m_pendingAfterTopOrdered then
        return
    end
    local list = self.m_pendingAfterTopOrdered
    self.m_pendingAfterTopOrdered = nil
    for _, action in ipairs(list) do
        action()
    end
end





ShopWeaponCtrl._ProcessArg = HL.Method(HL.Any) << function(self, arg)
    if arg == nil or string.isEmpty(arg.goodsId) then
        return
    end

    local goodsId = arg.goodsId
    arg.goodsId = nil
    local goods = Tables.shopGoodsTable[goodsId]
    local shopId = goods.shopId
    local goodsData = self.m_shopSystem:GetShopGoodsData(shopId, goodsId)
    if goodsData == nil then
        logger.error(ELogChannel.UI, "商店商品数据为空")
        return
    end

    if not self:_TrySetNaviFocusByGoodsId(goodsData.goodsId) then
        self.m_pendingNaviGoodsId = goodsData.goodsId
    end

    local isBox = string.isEmpty(goods.rewardId)
    if isBox then

        PhaseManager:OpenPhase(PhaseId.GachaWeaponPool, {goodsData = goodsData}, nil, true)
    else

        self.m_pendingAfterTopOrdered = self.m_pendingAfterTopOrdered or {}
        table.insert(self.m_pendingAfterTopOrdered, function()
            UIManager:Open(PanelId.ShopDetail, goodsData)
        end)
    end
end


ShopWeaponCtrl._GetGoodsDataInfoList = HL.Method(HL.Userdata).Return(HL.Table) << function(self, goodsDataList)
    local list = {}
    for i = 0, goodsDataList.Count - 1 do
        local goodsData = goodsDataList[i]







        table.insert(list, goodsData)
    end
    return list
end





ShopWeaponCtrl._InitShortCut = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self:BindInputPlayerAction("cashshop_navigation_4_dir_left", function()
        self:_OnGoLeft()
    end)

    self:BindInputPlayerAction("cashshop_navigation_4_dir_up", function()
        self:_OnGoUp()
    end)

    self:BindInputPlayerAction("cashshop_navigation_4_dir_right", function()
        self:_OnGoRight()
    end)

    self:BindInputPlayerAction("cashshop_navigation_4_dir_down", function()
        self:_OnGoDown()
    end)

end

ShopWeaponCtrl._OnGoLeft = HL.Method() << function(self)
    if self.m_currNaviCol == 1 then
        if self.m_currNaviRow == 1 then
            return
        end
        local prevRow = self.m_naviCellTable[self.m_currNaviRow - 1]
        local data = prevRow[#prevRow]
        self:_CustomNaviTarget(data)
        self.m_currNaviRow = self.m_currNaviRow - 1
        self.m_currNaviCol = #prevRow
        return
    end

    local table = self.m_naviCellTable[self.m_currNaviRow][self.m_currNaviCol - 1]
    self:_CustomNaviTarget(table)
    self.m_currNaviCol = self.m_currNaviCol - 1
end

ShopWeaponCtrl._OnGoUp = HL.Method() << function(self)
    if self.m_currNaviRow == 1 then
        return
    end

    local leftLength = self:_GetCurrLeftLength()
    local index, data = self:_FindCellByLeftLength(self.m_currNaviRow - 1, leftLength)
    if data then
        self:_CustomNaviTarget(data)
        self.m_currNaviRow = self.m_currNaviRow - 1
        self.m_currNaviCol = index
    end
end

ShopWeaponCtrl._OnGoRight = HL.Method() << function(self)
    local currRow = self.m_naviCellTable[self.m_currNaviRow]

    if self.m_currNaviCol == #currRow then
        if self.m_currNaviRow == #self.m_naviCellTable then
            return
        end
        local nextRow = self.m_naviCellTable[self.m_currNaviRow + 1]
        local data = nextRow[1]
        self:_CustomNaviTarget(data)
        self.m_currNaviRow = self.m_currNaviRow + 1
        self.m_currNaviCol = 1
        return
    end

    local table = currRow[self.m_currNaviCol + 1]
    self:_CustomNaviTarget(table)
    self.m_currNaviCol = self.m_currNaviCol + 1
end

ShopWeaponCtrl._OnGoDown = HL.Method() << function(self)
    logger.info("ShopWeaponCtrl._OnGoDown")

    if self.m_currNaviRow == #self.m_naviCellTable then
        return
    end

    local leftLength = self:_GetCurrLeftLength()
    local index, data = self:_FindCellByLeftLength(self.m_currNaviRow + 1, leftLength)
    if data then
        self:_CustomNaviTarget(data)
        self.m_currNaviRow = self.m_currNaviRow + 1
        self.m_currNaviCol = index
    end
end

ShopWeaponCtrl._CustomNaviTarget = HL.Method(HL.Any) << function(self, data)
    if data == nil then
        return
    end
    if self.m_currNaviData and self.m_currNaviData.naviNode and DeviceInfo.usingController then
        self.m_currNaviData.naviNode.gameObject:SetActive(false)
    end
    if data.cell then
        self:SetNaviTarget(data.cell)
    end
    if data.naviNode and DeviceInfo.usingController then
        data.naviNode.gameObject:SetActive(true)
    end
    self.m_currNaviData = data
end

ShopWeaponCtrl._TrySetNaviFocusByGoodsId = HL.Method(HL.Any).Return(HL.Boolean) << function(self, goodsId)
    if goodsId == nil then
        return false
    end
    for row, rowData in ipairs(self.m_naviCellTable) do
        for col, data in ipairs(rowData) do
            if data.goodsId == goodsId then
                self:_CustomNaviTarget(data)
                self.m_currNaviRow = row
                self.m_currNaviCol = col
                self.m_pendingNaviGoodsId = nil
                return true
            end
        end
    end
    return false
end

ShopWeaponCtrl._GetCurrLeftLength = HL.Method().Return(HL.Int) << function(self)
    local currRow = self.m_naviCellTable[self.m_currNaviRow]
    local leftLength = 0
    for i = 1, self.m_currNaviCol - 1 do
        local length = currRow[i].length
        leftLength = leftLength + length
    end
    return leftLength
end

ShopWeaponCtrl._FindCellByLeftLength = HL.Method(HL.Int, HL.Int).Return(HL.Any, HL.Any)
    << function(self, row, leftLength)
    local currRow = self.m_naviCellTable[row]
    local sum = 0
    for i = 1, #currRow do
        local left = sum
        local right = sum + currRow[i].length
        if leftLength >= left and leftLength < right then
            return i, currRow[i]
        end
        sum = right
    end
    return nil, nil
end





ShopWeaponCtrl._ComputeSeeGoods = HL.Method() << function(self)
    local viewPortRect = self.view.scroll.viewport
    for idx, line in ipairs(self.m_naviCellTable) do
        local firstCell = line[1].cell
        local vectorArray = CS.System.Array.CreateInstance(typeof(Vector3), 4)
        firstCell.gameObject:GetComponent("RectTransform"):GetWorldCorners(vectorArray)
        local leftUp = vectorArray[0]
        local leftDown = vectorArray[3]
        local leftUpLocalPos = viewPortRect.transform:InverseTransformPoint(leftUp)
        local leftDownLocalPos = viewPortRect.transform:InverseTransformPoint(leftDown)
        local leftUpVisible = viewPortRect.rect:Contains(leftUpLocalPos)
        local leftDownVisible = viewPortRect.rect:Contains(leftDownLocalPos)
        if leftUpVisible or leftDownVisible then
            if lume.find(self.m_haveSeenLines, idx) == nil then
                table.insert(self.m_haveSeenLines, idx)
            end
        end
    end
end



ShopWeaponCtrl._GetSingleWeaponCell = HL.Method().Return(HL.Any) << function(self)
    if self.view.singleWeaponCell ~= nil then
        return self.view.singleWeaponCell
    end
    local prefab = LuaSystemManager.cashShopItemPrefabSystem.weaponCaseCell
    if prefab == nil then
        return
    end
    local obj = CSUtils.CreateObject(prefab, self.view.randomWeaponsCase.weaponCellParent.transform)
    self.view.singleWeaponCell = Utils.wrapLuaNode(obj)
    self.view.singleWeaponCell.view.stateController:SetState("SingleMode")
    return self.view.singleWeaponCell
end

ShopWeaponCtrl._GetDoubleWeaponCell = HL.Method(HL.Number).Return(HL.Any) << function(self, index)
    local name = "doubleWeaponCell" .. index
    if self.view[name] ~= nil then
        return self.view[name]
    end
    local prefab = LuaSystemManager.cashShopItemPrefabSystem.weaponCaseCell
    if prefab == nil then
        return
    end
    local parent = index == 1 and self.view.doubleRandomWeaponsCase.weaponParent1 or self.view.doubleRandomWeaponsCase.weaponParent2
    local obj = CSUtils.CreateObject(prefab, parent.transform)
    self.view[name] = Utils.wrapLuaNode(obj)
    self.view[name].view.stateController:SetState("DoubleMode" .. index)
    return self.view[name]
end

ShopWeaponCtrl._ClearTopWeaponCells = HL.Method() << function(self)
    self:_DestroyTopWeaponCell("singleWeaponCell")
    self:_DestroyTopWeaponCell("doubleWeaponCell1")
    self:_DestroyTopWeaponCell("doubleWeaponCell2")
end

ShopWeaponCtrl._DestroyTopWeaponCell = HL.Method(HL.String) << function(self, name)
    local cell = self.view[name]
    if cell == nil then
        return
    end
    GameObject.Destroy(cell.gameObject)
    self.view[name] = nil
end

HL.Commit(ShopWeaponCtrl)
