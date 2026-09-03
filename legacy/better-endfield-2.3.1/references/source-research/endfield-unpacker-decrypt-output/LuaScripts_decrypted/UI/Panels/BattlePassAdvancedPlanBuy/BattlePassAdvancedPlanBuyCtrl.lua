
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BattlePassAdvancedPlanBuy
local PHASE_ID = PhaseId.BattlePassAdvancedPlanBuy

BattlePassAdvancedPlanBuyCtrl = HL.Class('BattlePassAdvancedPlanBuyCtrl', uiCtrl.UICtrl)






BattlePassAdvancedPlanBuyCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_BATTLE_PASS_TRACK_UPDATE] = '_AfterBuy',
}

BattlePassAdvancedPlanBuyCtrl.m_bpSystem = HL.Field(HL.Any)

BattlePassAdvancedPlanBuyCtrl.m_buyOriTrack = HL.Field(HL.Boolean) << false

BattlePassAdvancedPlanBuyCtrl.m_buyProtocalTrack = HL.Field(HL.Boolean) << false

BattlePassAdvancedPlanBuyCtrl.m_seasonData = HL.Field(HL.Any)

BattlePassAdvancedPlanBuyCtrl.m_oriPreInfos = HL.Field(HL.Any)

BattlePassAdvancedPlanBuyCtrl.m_payPreInfos = HL.Field(HL.Any)

BattlePassAdvancedPlanBuyCtrl.m_topCells = HL.Field(HL.Any)

BattlePassAdvancedPlanBuyCtrl.m_bottomCells = HL.Field(HL.Any)


BattlePassAdvancedPlanBuyCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)


    self:_InitData()


    self:_InitViews()


    self:_RefreshRewardsPreview()

    self.view.cashShopKrTips:InitCashShopKrTips()
end

BattlePassAdvancedPlanBuyCtrl._InitData = HL.Method() << function(self)
    self.m_bpSystem = GameInstance.player.battlePassSystem
    self.m_seasonData = BattlePassUtils.GetSeasonData()
    self:_RefreshTrackState()
end


BattlePassAdvancedPlanBuyCtrl._InitViews = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    local bpExpUpRatioOri = BattlePassUtils.GetOriginiumTrackInfo().bpExpUpRatio
    local bpExpUpRatioPro = BattlePassUtils.GetPayTrackInfo().bpExpUpRatio
    self.view.bpNumTxt1.text = string.format("+%d%%", bpExpUpRatioOri / 10)
    self.view.bpNumTxt2.text = string.format("+%d%%", bpExpUpRatioPro / 10)


    self.view.countDownText:InitCountDownText(self.m_bpSystem.seasonData.closeTime, nil, function(leftSec)
        return string.format(Language.LUA_BATTLEPASS_PLAN_END_TIME_HINT, UIUtils.getLeftTime(leftSec))
    end)


    self.m_topCells = UIUtils.genCellCache(self.view.topPreviewCell)
    self.m_bottomCells = UIUtils.genCellCache(self.view.bottomPreviewCell)


    self.view.walletBarPlaceholder:InitWalletBarPlaceholder({ Tables.battlePassConst.buyLevelMoneyID })


    self.view.oriAllRewardBtn.onClick:AddListener(function()
        PhaseManager:GoToPhase(PhaseId.BattlePassBuyPlan,{
            type = "Ori"
        })
    end)
    self.view.proAllRewardBtn.onClick:AddListener(function()
        PhaseManager:GoToPhase(PhaseId.BattlePassBuyPlan,{
            type = "Pro"
        })
    end)


    self.view.originiumTxt:SetAndResolveTextStyle(string.format("×%d", Tables.battlePassConst.buyOriginiumTrackMoneyCnt))
    self.view.originalOriginiumTxt.text = Tables.battlePassConst.buyOriginiumTrackOriPrice
    if Tables.battlePassConst.buyOriginiumTrackOriPrice == 0 then
        self.view.originalOriginiumTxt.gameObject:SetActive(false)
    end


    self.view.priceTxt.text = CashShopUtils.getGoodsPriceText(Tables.battlePassConst.buyPayTrackCashGoodsId)


    self.view.buyOriBtn.onClick:AddListener(function()
        BattlePassUtils.BuyOriginiumTrack()
    end)


    self.view.buyProBtn.onClick:AddListener(function()
        BattlePassUtils.BuyPayTrack()
    end)


    self.view.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "battle_pass")
    end)


    if BEYOND_DEBUG_COMMAND then
        UIUtils.bindInputEvent(CS.Beyond.Input.KeyboardKeyCode.Z, function()
            self:_AfterBuy()
        end, nil, nil, self.view.inputGroup.groupId)
    end

    self.view.originiumAllocateNode.onGroupSetAsNaviTarget:RemoveAllListeners()
    self.view.originiumAllocateNode.onGroupSetAsNaviTarget:AddListener(function(isTarget)
        self.view.originiumKeyHint.gameObject:SetActive(isTarget)
    end)
    self.view.agreementCustomizeNode.onGroupSetAsNaviTarget:RemoveAllListeners()
    self.view.agreementCustomizeNode.onGroupSetAsNaviTarget:AddListener(function(isTarget)
        self.view.agreementKeyHint.gameObject:SetActive(isTarget)
    end)

    self.view.originiumKeyHint.gameObject:SetActive(true)
    self.view.agreementKeyHint.gameObject:SetActive(false)
    if DeviceInfo.usingController then
        self:SetNaviTarget(self.view.originiumAllocateNode)
    end
end

BattlePassAdvancedPlanBuyCtrl._RefreshTrackState = HL.Method() << function(self)
    local isOrgTrackActive, playerOrgTrack = BattlePassUtils.CheckBattlePassTrackActive(GEnums.BPTrackType.ORIGINIUM)
    local useOrgTicket = isOrgTrackActive and playerOrgTrack ~= nil and playerOrgTrack.activeType == CS.Proto.BP_TRACK_ACTIVE_TYPE.BtatTicket
    local isPayTrackActive, playerPayTrack = BattlePassUtils.CheckBattlePassTrackActive(GEnums.BPTrackType.PAY)
    local usePayTicket = isPayTrackActive and playerPayTrack ~= nil and playerPayTrack.activeType == CS.Proto.BP_TRACK_ACTIVE_TYPE.BtatTicket
    self.m_buyOriTrack = isOrgTrackActive
    self.m_buyProtocalTrack = isPayTrackActive
    self.view.buyOriBtn.enabled = not self.m_buyOriTrack
    self.view.buyProBtn.enabled = not self.m_buyProtocalTrack
    self.view.buyOriBtnStateController:SetState(self.m_buyOriTrack and (useOrgTicket and "Ticket" or "Buy") or "NotBuy")
    self.view.buyProBtnStateController:SetState(self.m_buyProtocalTrack and (usePayTicket and "Ticket" or "Buy") or "NotBuy")
end

BattlePassAdvancedPlanBuyCtrl._AfterBuy = HL.Method() << function(self)
    local buyingPro = not self.m_buyProtocalTrack and BattlePassUtils.CheckPayTrackActive()
    self:_RefreshTrackState()
    self:_RefreshRewardsPreview()


    local shouldClose = self.m_buyProtocalTrack and self.m_buyOriTrack
    if buyingPro then
        BattlePassUtils.AfterBuyPayTrack(self, shouldClose, PHASE_ID)
    else
        BattlePassUtils.ShowTrackReward(GEnums.BPTrackType.ORIGINIUM, true, function()
            if shouldClose then
                PhaseManager:PopPhase(PHASE_ID)
            end
        end)
    end
end

BattlePassAdvancedPlanBuyCtrl._RefreshRewardsPreview = HL.Method() << function(self)
    self.m_oriPreInfos = self:_GetInfo(GEnums.BPTrackType.ORIGINIUM)
    self.m_payPreInfos = self:_GetInfo(GEnums.BPTrackType.PAY)

    local showInterval = 0.1
    if self.view.config.CELL_GRADUALLY_SHOW_INTERVAL ~= nil then
        showInterval = self.view.config.CELL_GRADUALLY_SHOW_INTERVAL
    end
    self.m_topCells:GraduallyRefresh(#self.m_oriPreInfos, showInterval, function(cell, index)
        cell.animationWrapper:PlayInAnimation()
        self:_OnUpdateCell(cell, self.m_oriPreInfos[index], index == #self.m_oriPreInfos)
    end)

    self.m_bottomCells:GraduallyRefresh(#self.m_payPreInfos, showInterval, function(cell, index)
        cell.animationWrapper:PlayInAnimation()
        self:_OnUpdateCell(cell, self.m_payPreInfos[index], index == #self.m_payPreInfos)
    end)
    AudioAdapter.PostEvent("Au_UI_Event_BPMotion")
end

BattlePassAdvancedPlanBuyCtrl._GetInfo = HL.Method(HL.Any).Return(HL.Table) << function(self, trackType)
    local ret = {}

    local previewId
    if trackType == GEnums.BPTrackType.ORIGINIUM then
        previewId = Tables.battlePassSeasonTable[self.m_bpSystem.seasonData.seasonId].originiumPreviewGroupId
    else
        previewId = Tables.battlePassSeasonTable[self.m_bpSystem.seasonData.seasonId].payPreviewGroupId
    end
    local infos = Tables.battlePassRewardPreviewTable[previewId].rewardInfos
    local isTrackActive, playerTrack = BattlePassUtils.CheckBattlePassTrackActive(trackType)
    local useTicket = isTrackActive and playerTrack ~= nil and playerTrack.activeType == CS.Proto.BP_TRACK_ACTIVE_TYPE.BtatTicket

    for i = 1,#infos do
        local info = infos[CSIndex(i)]
        if not useTicket or info.voucherInclusive == true then
            local temp = {
                name = info.name,
                desc = info.desc,
                iconId = info.iconId,
                itemId = info.itemId,
                count = info.count,
                finishLevel = info.finishLevel,
                canObtain = info.finishLevel == 0 and not isTrackActive,
                obtained = info.finishLevel == 0 and isTrackActive or BattlePassUtils.CheckIsRewardGained(trackType, info.finishLevel),
                sortId = info.sortId,
            }
            temp.obtainState = temp.obtained and 3 or (temp.canObtain and 1 or 2)
            table.insert(ret, temp)
        end
    end
    table.sort(ret, Utils.genSortFunction({"sortId"}, true))
    return ret
end

BattlePassAdvancedPlanBuyCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Table, HL.Boolean) << function(self, cell, info, isLast)

    cell.nameTxt.text = string.format(info.name, info.count)
    cell.lvTxt.text = string.format(info.desc, info.finishLevel)
    cell.icon:LoadSprite(UIConst.UI_SPRITE_BATTLE_PASS_PLAN, info.iconId)
    cell.decoLineNode.color = UIUtils.getItemRarityColor(Tables.itemTable[info.itemId].rarity)


    cell.stateController:SetState(info.obtained and "End" or (info.canObtain and "Obtain" or "LockLv"))


    cell.button.onClick:AddListener(function()
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            transform = cell.button.transform,
            posType = UIConst.UI_TIPS_POS_TYPE.RightTop,
            itemId = info.itemId,
            isSideTips = true,
        })
    end)


    cell.decoLineImage.gameObject:SetActive(not isLast)
end

BattlePassAdvancedPlanBuyCtrl.OnShow = HL.Override() << function(self)
end




BattlePassAdvancedPlanBuyCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.ON_BATTLE_PASS_ADVANCED_BUY_CLOSE)
end




HL.Commit(BattlePassAdvancedPlanBuyCtrl)
