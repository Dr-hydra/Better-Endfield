
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BattlePassBuyPlan
local PHASE_ID = PhaseId.BattlePassBuyPlan

BattlePassBuyPlanCtrl = HL.Class('BattlePassBuyPlanCtrl', uiCtrl.UICtrl)






BattlePassBuyPlanCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_BATTLE_PASS_TRACK_UPDATE] = '_AfterBuy',
}

local itemPerLine = 7

BattlePassBuyPlanCtrl.m_bpSystem = HL.Field(HL.Any)

BattlePassBuyPlanCtrl.m_buyOriTrack = HL.Field(HL.Boolean) << false

BattlePassBuyPlanCtrl.m_buyProtocalTrack = HL.Field(HL.Boolean) << false

BattlePassBuyPlanCtrl.m_seasonData = HL.Field(HL.Any)

BattlePassBuyPlanCtrl.m_rewardNow = HL.Field(HL.Table)

BattlePassBuyPlanCtrl.m_rewardFuture = HL.Field(HL.Table)

BattlePassBuyPlanCtrl.m_rewardNowCells = HL.Field(HL.Any)

BattlePassBuyPlanCtrl.m_rewardFutureCells = HL.Field(HL.Any)

BattlePassBuyPlanCtrl.m_isOri = HL.Field(HL.Boolean) << true

BattlePassBuyPlanCtrl.m_onClose = HL.Field(HL.Function)


BattlePassBuyPlanCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.view.closeButton.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)


    self.m_isOri = arg.type == "Ori"
    self.m_onClose = arg.onClose
    self.m_bpSystem = GameInstance.player.battlePassSystem
    self.m_seasonData = BattlePassUtils.GetSeasonData()
    self.m_buyOriTrack = BattlePassUtils.CheckOriginiumTrackActive()
    self.m_buyProtocalTrack = BattlePassUtils.CheckPayTrackActive()


    if self.m_isOri then
        self.view.walletBarPlaceholder:InitWalletBarPlaceholder({ Tables.battlePassConst.buyLevelMoneyID })
    end


    self.view.costTotalTxt.text = Tables.battlePassConst.buyOriginiumTrackMoneyCnt
    self.view.scorePriceTxt.text = Tables.battlePassConst.buyOriginiumTrackOriPrice
    if Tables.battlePassConst.buyOriginiumTrackOriPrice == 0 then
        self.view.scorePriceTxt.gameObject:SetActive(false)
    end
    self.view.actualPriceTxt.text = CashShopUtils.getGoodsPriceText(Tables.battlePassConst.buyPayTrackCashGoodsId)


   self:_RefreshState()


    self.view.buyButton.onClick:AddListener(function()
        self:_BuyPlan()
    end)


    self:_InitReward()


    local bpExpUpRatioOri = BattlePassUtils.GetOriginiumTrackInfo().bpExpUpRatio / 10
    local bpExpUpRatioPro = BattlePassUtils.GetPayTrackInfo().bpExpUpRatio / 10
    self.view.titleTxt.text = self.m_isOri and Language.LUA_BATTLEPASS_ORI_TRACK_PREVIEW_TITLE or Language.LUA_BATTLEPASS_BUY_TRACK_PREVIEW_TITLE
    self.view.infoTitleTxt.text = self.m_isOri and Language.LUA_BATTLEPASS_ORI_TRACK_PREVIEW_INFO_TITLE or Language.LUA_BATTLEPASS_BUY_TRACK_PREVIEW_INFO_TITLE
    self.view.infoSubTitleTxt.text = self.m_isOri and Language.LUA_BATTLEPASS_ORI_TRACK_PREVIEW_INFO_SUBTITLE or Language.LUA_BATTLEPASS_BUY_TRACK_PREVIEW_INFO_SUBTITLE
    self.view.moreExpText.text = string.format("+%d%%", self.m_isOri and bpExpUpRatioOri or bpExpUpRatioPro)


    local _, bannerGroup = Tables.battlePassBannerTable:TryGetValue(self.m_seasonData.bannerPresetId)
    for _, bannerData in pairs(bannerGroup.bannerInfos) do
        if bannerData.trackId == (self.m_isOri and Tables.battlePassTrackTypeToIDTable[GEnums.BPTrackType.ORIGINIUM].bpTrackID or Tables.battlePassTrackTypeToIDTable[GEnums.BPTrackType.PAY].bpTrackID)
            and bannerData.itemId == "" then
            self.view.caseImg:LoadSprite(UIConst.UI_SPRITE_BATTLE_PASS_PLAN, bannerData.iconId)
            break
        end
    end
end

BattlePassBuyPlanCtrl._AfterBuy = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    local buyingPro = not self.m_buyProtocalTrack and BattlePassUtils.CheckPayTrackActive()

    if buyingPro then
        BattlePassUtils.AfterBuyPayTrack(self, true, PHASE_ID)
    else
        PhaseManager:PopPhase(PHASE_ID)
    end

end

BattlePassBuyPlanCtrl._RefreshState = HL.Method() << function(self)
    local alreadyBuy
    if self.m_isOri then
        alreadyBuy = self.m_buyOriTrack
    else
        alreadyBuy = self.m_buyProtocalTrack
    end
    local moneyEnough = Utils.getItemCount(Tables.battlePassConst.buyOriginiumTrackMoneyID, false) >= Tables.battlePassConst.buyOriginiumTrackMoneyCnt
    self.view.costHint.gameObject:SetActive(self.m_isOri and (not alreadyBuy) and (not moneyEnough))
    self.view.buyButton.enabled = not alreadyBuy
    self.view.root:SetState(alreadyBuy and "DisableState" or "NormalState")
    if alreadyBuy then
        self.view.amountNode:SetState("Active")
    elseif self.m_isOri and moneyEnough then
        self.view.amountNode:SetState("Originium")
    elseif self.m_isOri then
        self.view.amountNode:SetState("NoOriginium")
    else
        self.view.amountNode:SetState("Pay")
    end
end

BattlePassBuyPlanCtrl._InitReward = HL.Method() << function(self)
    local maxLevel = self.m_seasonData.maxLevel
    local levelGroupId = self.m_seasonData.levelGroupId
    local overrideLevelGroupId = self.m_seasonData.ovrLvRewardGroupId


    self.m_rewardNow = {}
    self.m_rewardNowCells = UIUtils.genCellCache(self.view.rewardNow)
    if self.m_isOri then
        self.view.rewardNode.gameObject:SetActive(false)
        self.m_rewardNowCells:Refresh(0, nil)
    else
        self.view.rewardNode.gameObject:SetActive(true)
        local _, goodsData = Tables.cashShopGoodsTable:TryGetValue(Tables.battlePassConst.buyPayTrackCashGoodsId)
        local rewardId = goodsData.rewardId
        self.m_rewardNow = UIUtils.getRewardItemsMergeSameId(rewardId, self.m_rewardNow)
        for i = 1, #self.m_rewardNow do
            self.m_rewardNow[i].rarity = Tables.itemTable[self.m_rewardNow[i].id].rarity
        end
        table.sort(self.m_rewardNow, Utils.genSortFunction({"rarity"}, false))
        self.m_rewardNowCells:Refresh(#self.m_rewardNow, function(cell, index)
            cell:InitItem(self.m_rewardNow[index],function()
                Notify(MessageConst.SHOW_ITEM_TIPS, {
                    transform = cell.transform,
                    posType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
                    itemId = self.m_rewardNow[index].id,
                    isSideTips = true,
                })
            end)
            cell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        end)
        self.view.keyHintDown.gameObject:SetActive(false)
    end


    self:_StartCoroutine(function()
        coroutine.step()
        coroutine.step()
        self.view.rewardList.gameObject:SetActive(false)
        self.view.rewardList.gameObject:SetActive(true)
    end)


    self.m_rewardFuture = {}
    self.m_rewardFutureCells = UIUtils.genCellCache(self.view.rewardNodeDown)
    for i = 1, maxLevel do
        local levelInfo = Tables.battlePassLevelTable[levelGroupId].levelInfos[i]
        local rewardId = self.m_isOri and levelInfo.originiumRewardId or levelInfo.payRewardId
        local overrideLevelData = BattlePassUtils.GetBattlePassOverrideLevelData(levelInfo.level, overrideLevelGroupId)
        if overrideLevelData ~= nil then
            rewardId = self.m_isOri and overrideLevelData.originiumRewardId or overrideLevelData.payRewardId
        end
        self.m_rewardFuture = UIUtils.getRewardItemsMergeSameId(rewardId, self.m_rewardFuture)
    end
    for i = 1, #self.m_rewardFuture do
        self.m_rewardFuture[i].rarity = Tables.itemTable[self.m_rewardFuture[i].id].rarity
    end
    table.sort(self.m_rewardFuture, Utils.genSortFunction({"rarity"}, false))


    self.m_rewardFutureCells:Refresh(#self.m_rewardFuture / itemPerLine, function(cell, index)

        local itemThisLine = math.min(itemPerLine ,#self.m_rewardFuture - itemPerLine * (index - 1))
        UIUtils.genCellCache(cell.rewardFuture):Refresh(itemThisLine, function(cell, innerIndex)
            local realIndex = itemPerLine * (index - 1) + innerIndex
            cell:InitItem(self.m_rewardFuture[realIndex],function()
                Notify(MessageConst.SHOW_ITEM_TIPS, {
                    transform = cell.transform,
                    posType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
                    itemId = self.m_rewardFuture[realIndex].id,
                    isSideTips = true,
                })
            end)
            cell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        end)
    end)
end

BattlePassBuyPlanCtrl._BuyPlan = HL.Method() << function(self)
    if self.m_isOri then
        BattlePassUtils.BuyOriginiumTrack()
    else
        BattlePassUtils.BuyPayTrack()
    end
end

BattlePassBuyPlanCtrl.OnShow = HL.Override() << function(self)
    self:_RefreshState()
end



BattlePassBuyPlanCtrl.OnClose = HL.Override() << function(self)
    if self.m_onClose ~= nil then
        self.m_onClose()
    end
end




HL.Commit(BattlePassBuyPlanCtrl)
