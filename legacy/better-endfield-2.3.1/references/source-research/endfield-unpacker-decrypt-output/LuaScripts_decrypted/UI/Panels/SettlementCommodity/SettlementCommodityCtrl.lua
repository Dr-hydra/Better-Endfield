local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SettlementCommodity
local PHASE_ID = PhaseId.SettlementCommodity
SettlementCommodityCtrl = HL.Class('SettlementCommodityCtrl', uiCtrl.UICtrl)






SettlementCommodityCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = '_OnActivityStageUpdate',
}



SettlementCommodityCtrl.m_genTradeItemCellFunc = HL.Field(HL.Function)

SettlementCommodityCtrl.m_stlId = HL.Field(HL.String) << ""

SettlementCommodityCtrl.m_stlLevel = HL.Field(HL.Number) << 0

SettlementCommodityCtrl.m_curSelectedItemIndex = HL.Field(HL.Number) << 0

SettlementCommodityCtrl.m_curSellItemId = HL.Field(HL.String) << ""

SettlementCommodityCtrl.m_curRecommendItemId = HL.Field(HL.String) << ""

SettlementCommodityCtrl.m_itemDataList = HL.Field(HL.Table)

SettlementCommodityCtrl.m_onConfirmChangedCallback = HL.Field(HL.Function)


SettlementCommodityCtrl.m_activityInfo = HL.Field(HL.Table)














SettlementCommodityCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self:_InitData(arg)
    self:_UpdateData()
    self:_ApplyResumeState(arg and arg.resumeState or nil)
    self:_RefreshAllUI()
end

SettlementCommodityCtrl.OnAnimationInFinished = HL.Override() << function(self)
    local firstCell = self.m_genTradeItemCellFunc(self.m_curSelectedItemIndex)
    if firstCell then
        self:SetNaviTarget(firstCell.cellBtn)
    end
end




SettlementCommodityCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_stlId = arg.settlementId
    self.m_stlLevel = arg.settlementLevel
    self.m_onConfirmChangedCallback = arg.onConfirmChanged

    self.m_curSelectedItemIndex = 1
    self.m_curSellItemId = arg.curSellItem or GameInstance.player.settlementSystem:GetCurSellItem(self.m_stlId)

    local hasCfg, basicData = Tables.settlementBasicDataTable:TryGetValue(self.m_stlId)
    if not hasCfg then
        logger.error("not exist table cfg, id: " .. self.m_stlId)
        return
    end
    local levelData = basicData.settlementLevelMap[self.m_stlLevel]
    self.m_curRecommendItemId = levelData.recoItemId
end

SettlementCommodityCtrl._UpdateData = HL.Method() << function(self)
    self.m_itemDataList = {}
    local hasCfg, stlCfg = Tables.settlementBasicDataTable:TryGetValue(self.m_stlId)
    if not hasCfg then
        logger.error("not exist table cfg, id: " .. self.m_stlId)
        return
    end
    local stlId = self.m_stlId
    local domainId = stlCfg.domainId
    local domainCfg = Tables.domainDataTable[domainId]
    local moneyId = domainCfg.domainGoldItemId
    local moneyItemData = Tables.itemTable[moneyId]
    local moneyIcon = moneyItemData.iconId
    local stlData = GameInstance.player.settlementSystem:GetUnlockSettlementData(stlId)
    local stlLevelCfg = stlCfg.settlementLevelMap[stlData.level]

    self.m_activityInfo = DomainPOIUtils.getSettlementTradeActivityInfo()
    local hasActivity = self.m_activityInfo.hasActivity and
        self.m_activityInfo.domainActivityInfos[domainId] ~= nil and
        self.m_activityInfo.domainActivityInfos[domainId][stlId] ~= nil
    local stlActivityInfo
    if hasActivity then
        stlActivityInfo = self.m_activityInfo.domainActivityInfos[domainId][stlId]
    end


    local levelData = stlCfg.settlementLevelMap[self.m_stlLevel]
    for itemId, tradeItemData in pairs(levelData.settlementTradeItemMap) do
        local isCurSell = itemId == self.m_curSellItemId
        local isRecommend = itemId == self.m_curRecommendItemId
        local stlItemTradeCfg = stlLevelCfg.settlementTradeItemMap[itemId]
        local itemData = Tables.itemTable[itemId]
        local localCount = Utils.getDepotItemCount(itemId, Utils.getCurrentScope(), domainId)

        local rewardActivityMoneyCount = 0



        local isActivityItem = not string.isEmpty(stlItemTradeCfg.activityId)
        local isHide = false
        if isActivityItem then
            isHide = not hasActivity or not GameInstance.player.inventory:IsItemFound(itemId)
        else
            isHide = not Utils.isCurTimeInTimeIdRange(stlItemTradeCfg.timeId, true)
        end
        if stlActivityInfo and stlActivityInfo[itemId] ~= nil then
            rewardActivityMoneyCount = stlActivityInfo[itemId]
        end


        if not isHide then
            local tradeItemBundle = {
                itemId = itemId,
                itemName = itemData.name,
                itemIcon = itemData.iconId,
                rarity = itemData.rarity,
                localCount = localCount,
                price = tradeItemData.rewardMoneyCount,
                moneyIcon = moneyIcon,
                isCurSell = isCurSell,
                isActivityItem = isActivityItem,
                isRecommend = isRecommend,
                rewardActivityMoneyCount = rewardActivityMoneyCount,

                recommendOrder = isRecommend and 1 or 0,
                isCurSellOrder = isCurSell and 1 or 0,
                activityOrder = isActivityItem and 1 or 0,
            }
            table.insert(self.m_itemDataList, tradeItemBundle)
        end
    end







    table.sort(self.m_itemDataList, Utils.genSortFunction({ "isCurSellOrder", "activityOrder", "recommendOrder", "price", "localCount", "itemId" }))
end




SettlementCommodityCtrl._InitUI = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.view.closeBtn.onClick:AddListener(function()
        self:_CloseSelf()
    end)
    self.view.confirmBtn.onClick:AddListener(function()
        self:_ConfirmChangeItem()
    end)

    self.m_genTradeItemCellFunc = UIUtils.genCachedCellFunction(self.view.tradeItemList)
    self.view.tradeItemList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_genTradeItemCellFunc(obj)
        self:_RefreshTradeItemCell(cell, LuaIndex(csIndex))
    end)
end

SettlementCommodityCtrl._RefreshAllUI = HL.Method() << function(self)
    local onePageCount = self.view.config.ONE_PAGE_ITEM_COUNT
    local oneLineCount = self.view.config.ONE_LINE_ITEM_COUNT
    local itemCount = #self.m_itemDataList
    local realCount = itemCount


    if itemCount < onePageCount then
        realCount = onePageCount
    end

    local remainder = realCount % oneLineCount
    if remainder ~= 0 then
        realCount = realCount + (oneLineCount - remainder)
    end

    self.view.tradeItemList:UpdateCount(realCount, true)
end

SettlementCommodityCtrl._RefreshTradeItemCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    cell.tipsBtn.onClick:RemoveAllListeners()
    cell.cellBtn.onClick:RemoveAllListeners()
    cell.cellBtn.onLongPress:RemoveAllListeners()
    local totalCount = #self.m_itemDataList
    if luaIndex > totalCount then
        cell.gameObject.name = "Empty_" .. luaIndex
        cell.nodeStateCtrl:SetState("EmptyState")
        cell.curSellStateCtrl:SetState("notCurSell")
        cell.markStateCtrl:SetState("notRecommend")
        cell.markStateCtrl:SetState("notActivity")
        cell.cellBtn.interactable = false
        return
    end

    local tradeItemBundle = self.m_itemDataList[luaIndex]
    local itemId = tradeItemBundle.itemId
    local activityMoneyId = self.m_activityInfo.activityMoneyId
    cell.gameObject.name = itemId
    cell.itemIconImg:LoadSprite(UIConst.UI_SPRITE_ITEM, tradeItemBundle.itemIcon)
    cell.moneyImg:LoadSprite(UIConst.UI_SPRITE_WALLET, tradeItemBundle.moneyIcon)
    cell.itemNameTxt.text = tradeItemBundle.itemName
    cell.localItemNumTxt.text = UIUtils.getNumString(tradeItemBundle.localCount)
    cell.localItemNumTxt.color = tradeItemBundle.localCount <= 0 and self.view.config.NUM_COLOR_NOT_ENOUGH or self.view.config.NUM_COLOR_ENOUGH
    cell.priceTxt.text = tradeItemBundle.price
    cell.rarityImg.color = UIUtils.getItemRarityColor(tradeItemBundle.rarity)
    if tradeItemBundle.isActivityItem then
        cell.activityMoneyImg:LoadSprite(UIConst.UI_SPRITE_WALLET, activityMoneyId)
        cell.activityPriceTxt.text = tradeItemBundle.rewardActivityMoneyCount
        cell.activityMarkImg.color = self.m_activityInfo.activityColor
    end

    if self.m_curSelectedItemIndex ~= luaIndex then
        cell.nodeStateCtrl:SetState("NormalState")
    else
        cell.nodeStateCtrl:SetState("SelectState")
    end
    if tradeItemBundle.isCurSell then
        cell.curSellStateCtrl:SetState("isCurSell")
    else
        cell.curSellStateCtrl:SetState("notCurSell")
    end
    if tradeItemBundle.isRecommend then
        cell.markStateCtrl:SetState("isRecommend")
    else
        cell.markStateCtrl:SetState("notRecommend")
    end
    cell.markStateCtrl:SetState(tradeItemBundle.isActivityItem and "isActivity" or "notActivity")

    cell.cellBtn.interactable = true
    cell.tipsBtn.onClick:AddListener(function()
        self:_ChangeCurSelectedItem(luaIndex)
        self:_OpenTradeItemTips(itemId, cell.transform)
    end)
    cell.cellBtn.onLongPress:AddListener(function()
        self:_ChangeCurSelectedItem(luaIndex)
        self:_OpenTradeItemTips(itemId, cell.transform)
    end)
    cell.cellBtn.onClick:AddListener(function()
        self:_ChangeCurSelectedItem(luaIndex)
    end)
    if DeviceInfo.usingController then
        cell.inputGroup.enabled = false
    end
    cell.cellBtn.onIsNaviTargetChanged = function(isNaviTarget, _, _)
        if DeviceInfo.usingController then
            cell.inputGroup.enabled = isNaviTarget
        end
        if isNaviTarget then
            self:_ChangeCurSelectedItem(luaIndex)
        end
    end
end




SettlementCommodityCtrl._CloseSelf = HL.Method() << function(self)
    PhaseManager:PopPhase(PHASE_ID)
end

SettlementCommodityCtrl._ConfirmChangeItem = HL.Method() << function(self)
    if self.m_onConfirmChangedCallback then
        local index = self.m_curSelectedItemIndex
        if index > 0 and index <= #self.m_itemDataList then
            local id = self.m_itemDataList[index].itemId
            if id ~= self.m_curSellItemId then
                self.m_onConfirmChangedCallback(id)
            end
        end
    end
    self:_CloseSelf()
end

SettlementCommodityCtrl._OpenTradeItemTips = HL.Method(HL.String, Transform) << function(self, itemId, transform)
    Notify(MessageConst.SHOW_ITEM_TIPS, {
        transform = transform,
        posType = UIConst.UI_TIPS_POS_TYPE.RightDown,
        itemId = itemId,
    })
end

SettlementCommodityCtrl._ChangeCurSelectedItem = HL.Method(HL.Number) << function(self, luaIndex)
    if self.m_curSelectedItemIndex == luaIndex then
        return
    end
    local obj = self.view.tradeItemList:Get(CSIndex(self.m_curSelectedItemIndex))
    local oldCell = self.m_genTradeItemCellFunc(obj)
    if oldCell then
        oldCell.nodeStateCtrl:SetState("NormalState")
    end
    obj = self.view.tradeItemList:Get(CSIndex(luaIndex))
    local cell = self.m_genTradeItemCellFunc(obj)
    if cell then
        cell.nodeStateCtrl:SetState("SelectState")
    end
    self.m_curSelectedItemIndex = luaIndex
end

SettlementCommodityCtrl._ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if not resumeState then
        return
    end

    if not string.isEmpty(resumeState.selectedItemId) then
        for luaIndex, itemData in ipairs(self.m_itemDataList) do
            if itemData.itemId == resumeState.selectedItemId then
                self.m_curSelectedItemIndex = luaIndex
                return
            end
        end
    end

    local selectedIndex = resumeState.selectedIndex
    if selectedIndex and selectedIndex > 0 and selectedIndex <= #self.m_itemDataList then
        self.m_curSelectedItemIndex = selectedIndex
    end
end

SettlementCommodityCtrl._OnActivityStageUpdate = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if self.m_activityInfo == nil or self.m_activityInfo.activityId ~= activityId then
        return
    end
    self:_UpdateData()
    self:_RefreshAllUI()
end

SettlementCommodityCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local curSelectedItem = self.m_itemDataList and self.m_itemDataList[self.m_curSelectedItemIndex] or nil
    return {
        settlementId = self.m_stlId,
        settlementLevel = self.m_stlLevel,
        curSellItem = self.m_curSellItemId,
        onConfirmChanged = self.m_onConfirmChangedCallback,
        resumeState = {
            selectedIndex = self.m_curSelectedItemIndex,
            selectedItemId = curSelectedItem and curSelectedItem.itemId or "",
        }
    }
end


HL.Commit(SettlementCommodityCtrl)
