
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonMoneyExchange
local PHASE_ID = PhaseId.CommonMoneyExchange
CommonMoneyExchangeCtrl = HL.Class('CommonMoneyExchangeCtrl', uiCtrl.UICtrl)





CommonMoneyExchangeCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_WALLET_CHANGED] = 'Refresh',
    [MessageConst.ON_ITEM_COUNT_CHANGED] = 'Refresh',
    [MessageConst.ON_SHOP_MONEY_EXCHANGE_SUCC] = 'Success',
}

CommonMoneyExchangeCtrl.m_arg = HL.Field(HL.Table)

CommonMoneyExchangeCtrl.m_weaponGachaMoneyCount = HL.Field(HL.Number) << 0

CommonMoneyExchangeCtrl.m_weaponDecreaseNumber = HL.Field(HL.Number) << 0

CommonMoneyExchangeCtrl.m_weaponIncreaseNumber = HL.Field(HL.Number) << 0






CommonMoneyExchangeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg
    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.view.mask.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.view.tipsBtn1.onClick:AddListener(function()
        local sourceId = self.m_arg.sourceId
		local args = {
			itemId = sourceId,
			transform = self.view.tipsBtn1.transform,
            onBeforeJump = function()
                PhaseManager:ExitPhaseFast(PHASE_ID)
            end,
		}
		if DeviceInfo.usingController then
			args.posType = UIConst.UI_TIPS_POS_TYPE.RightMid
			args.isSideTips = true
			args.notPenetrate = false
		end
		Notify(MessageConst.SHOW_ITEM_TIPS, args)
    end)

    self.view.tipsBtn2.onClick:AddListener(function()
        local targetId = self.m_arg.targetId
		local args = {
			itemId = targetId,
			transform = DeviceInfo.usingController and self.view.icon2.transform or self.view.tipsBtn2.transform,
            onBeforeJump = function()
                PhaseManager:ExitPhaseFast(PHASE_ID)
            end,
		}
		if DeviceInfo.usingController then
			args.posType = UIConst.UI_TIPS_POS_TYPE.LeftMid
			args.isSideTips = true
			args.notPenetrate = false
		end
		Notify(MessageConst.SHOW_ITEM_TIPS, args)
    end)
    local b, config = CS.Beyond.Gameplay.ShopSystem.GetExchangeData(arg.sourceId, arg.targetId)
    if not b then
        logger.error("can not find money exchange data")
        return
    end
    self.view.confirmButton.onClick:AddListener(function()
        self:_OnConfirmBtnClick()
    end)
    self.view.gotoRechargeButton.onClick:AddListener(function()
        self:_OnGoToRechargeBtnClick()
    end)


    self.view.numWeaponNode.minButton.onClick:AddListener(function()
        self:_OnClickWeaponDecreaseBtn()
    end)
    self.view.numWeaponNode.maxButton.onClick:AddListener(function()
        self:_OnClickWeaponIncreaseBtn()
    end)


    self.view.exchangeNodeSelectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        local state = isFocused and CS.Beyond.UI.CustomUIStyle.OverrideValidState.ForceNotValid
            or CS.Beyond.UI.CustomUIStyle.OverrideValidState.None
        self.view.numberSelector.view.addButton.transform:Find("KeyHint"):GetComponent("CustomUIStyle").overrideValidState = state
        self.view.numberSelector.view.reduceButton.transform:Find("KeyHint"):GetComponent("CustomUIStyle").overrideValidState = state
    end)

    if self.m_arg.targetId == Tables.globalConst.gachaWeaponItemId then
        self:_InitWeaponGachaMoneyCount()
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self:Refresh()
end

CommonMoneyExchangeCtrl.Success = HL.Method(HL.Any) << function(self, msg)
    local items = {}
    local reward = unpack(msg)
    local item = {
        id = reward.TargetMoneyId,
        count = reward.GetTargetMoneyNum,
    }
    table.insert(items, item)

    PhaseManager:ExitPhaseFast(PHASE_ID)
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        title = Language.LUA_BUY_ITEM_SUCC_TITLE,
        icon = "icon_mail_obtain",
        items = items,
    })
end

CommonMoneyExchangeCtrl.Refresh = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    local sourceId = self.m_arg.sourceId
    local targetId = self.m_arg.targetId
    local item1 = Tables.itemTable[sourceId]
    local item2 = Tables.itemTable[targetId]
    self.view.nameTxt1.text = item1.name
    self.view.nameTxt2.text = item2.name
    self.view.title.text = string.format(Language.LUA_SHOP_MONEY_EXCHANGE_TITLE, item2.name)
    self.view.icon1:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, item1.iconId)
    self.view.icon2:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, item2.iconId)

    local b, config = CS.Beyond.Gameplay.ShopSystem.GetExchangeData(sourceId, targetId)
    if not b then
        logger.error("can not find money exchange data")
        return
    end
    self.view.costNumTxt1.text = math.floor(config.sourceMoneyMinSwap)

    self.view.costNumTxt2.text = math.floor(config.targetMoneyGet * config.sourceMoneyMinSwap)

    self.view.totalNumTxt1.text = Utils.getItemCount(sourceId)
    self.view.totalNumTxt2.text = Utils.getItemCount(targetId)

    self.view.money1.text = string.format("*%s [%s]", config.sourceMoneyCost, item1.name)
    self.view.money2.text = string.format("*%s [%s]", config.targetMoneyGet, item2.name)
    local max = math.max(1, math.floor(Utils.getItemCount(sourceId) / config.sourceMoneyMinSwap))
    local canExchange = math.floor(Utils.getItemCount(sourceId) / config.sourceMoneyCost) > 0
    local canGotoRecharge = not canExchange and sourceId == Tables.globalConst.originiumItemId
    if canGotoRecharge then
        self.view.gotoRechargeButton.gameObject:SetActive(true)
        self.view.confirmButton.gameObject:SetActive(false)
        self.view.confirmTxt.text = string.format(Language.LUA_COMMON_MONEY_EXCHANGE_GOTO_RECHARGE_CONFIRMTXT)
    else
        self.view.gotoRechargeButton.gameObject:SetActive(false)
        self.view.confirmButton.gameObject:SetActive(true)
        self.view.confirmButton.interactable = canExchange
        if not canExchange then
            self.view.confirmTxt.text = string.format(Language.LUA_SHOP_BUY_MONEY_NOT_ENOUGH, item1.name)
        end
    end
    local curNum = self.view.numberSelector.curNumber
    if self.m_arg.initExchangeCount then
        curNum = self.m_arg.initExchangeCount
        self.m_arg.initExchangeCount = nil
    end
    self.view.numberSelector:InitNumberSelector(curNum, 1, max, function(newNum)
        local costOriginNum = math.floor(newNum * config.sourceMoneyMinSwap)
        local getTargetNum = math.floor((newNum * config.sourceMoneyMinSwap / config.sourceMoneyCost) * config.targetMoneyGet)
        self.view.costNumTxt1.text = costOriginNum
        self.view.costNumTxt2.text = getTargetNum
        self.view.numberSelector.view.numberText.text = math.floor(newNum)
        if canGotoRecharge then
            self.view.confirmTxt.text = string.format(Language.LUA_COMMON_MONEY_EXCHANGE_GOTO_RECHARGE_CONFIRMTXT)
        else
            self.view.confirmTxt.text = string.format(Language.LUA_SHOP_MONEY_EXCHANGE_TIPS, item1.name .. "×" .. math.floor(newNum * config.sourceMoneyMinSwap), item2.name .. "×" ..
                math.floor((newNum * config.sourceMoneyMinSwap / config.sourceMoneyCost) * config.targetMoneyGet))
        end
        if targetId == Tables.globalConst.gachaWeaponItemId then
            self:_RefreshWeaponNode(newNum)
        end
    end)


    if targetId == Tables.globalConst.gachaWeaponItemId then
        self.view.numberSelector.view.maxButton.transform:GetComponent("CustomUIStyle").overrideValidState = CS.Beyond.UI.CustomUIStyle.OverrideValidState.ForceNotValid
        self.view.numberSelector.view.minButton.transform:GetComponent("CustomUIStyle").overrideValidState = CS.Beyond.UI.CustomUIStyle.OverrideValidState.ForceNotValid
    else
        self.view.numberSelector.view.maxButton.transform:GetComponent("CustomUIStyle").overrideValidState = CS.Beyond.UI.CustomUIStyle.OverrideValidState.None
        self.view.numberSelector.view.minButton.transform:GetComponent("CustomUIStyle").overrideValidState = CS.Beyond.UI.CustomUIStyle.OverrideValidState.None
    end


    if targetId == Tables.globalConst.gachaWeaponItemId then
        self.view.numWeaponNode.gameObject:SetActive(true)
    else
        self.view.numWeaponNode.gameObject:SetActive(false)
    end
end

CommonMoneyExchangeCtrl._OnConfirmBtnClick = HL.Method() << function(self)
    local ret, error = CS.Beyond.Gameplay.ShopSystem.ExchangeMoney(self.m_arg.sourceId, self.m_arg.targetId, math.floor(tonumber(self.view.costNumTxt1.text)))
    logger.info(tostring(ret))
end

CommonMoneyExchangeCtrl._InitWeaponGachaMoneyCount = HL.Method() << function(self)
    local _, box, goods = GameInstance.player.shopSystem:GetNowUpWeaponData()
    local count = box == nil and 0 or box.Count
    if count > 0 then
        local goodsData = box[0]
        local costInfo = CashShopUtils.TryGetBuyGachaWeaponGoodsCostInfo(goodsData.shopId, goodsData.goodsId)
        self.m_weaponGachaMoneyCount = costInfo.costMoneyCount
    else
        self.m_weaponGachaMoneyCount = 1980
    end
end

CommonMoneyExchangeCtrl._RefreshWeaponNode = HL.Method(HL.Number)
    << function(self, selectorNum)
    local sourceId = self.m_arg.sourceId
    local targetId = self.m_arg.targetId
    local existCfg, config = CS.Beyond.Gameplay.ShopSystem.GetExchangeData(sourceId, targetId)
    if not existCfg then
        logger.error("can not find money exchange data")
        return
    end

    local sourceNum = Utils.getItemCount(sourceId)

    local targetNum = Utils.getItemCount(targetId)

    local costOriginNum = math.floor(selectorNum * config.sourceMoneyMinSwap)

    local getTargetNum = math.floor((selectorNum * config.sourceMoneyMinSwap / config.sourceMoneyCost) * config.targetMoneyGet)

    local weaponNumStep = math.floor((1 * config.sourceMoneyMinSwap / config.sourceMoneyCost) * config.targetMoneyGet)

    local minWeaponNum = targetNum + weaponNumStep

    local minWeaponGachaCount = math.floor(minWeaponNum / self.m_weaponGachaMoneyCount)

    local firstBiggerWeaponNum = (minWeaponGachaCount + 1) * self.m_weaponGachaMoneyCount
    local firstStepCount = math.ceil((firstBiggerWeaponNum - minWeaponNum) / weaponNumStep)
    local firstNumberSelectorNumThreshold = firstStepCount + 1

    local afterCurrSelectSourceNum = sourceNum - costOriginNum

    local afterCurrSelectWeaponNum = targetNum + getTargetNum

    local currSelectWeaponGachaCount = math.floor(afterCurrSelectWeaponNum / self.m_weaponGachaMoneyCount)

    local nextBiggerWeaponNum = (currSelectWeaponGachaCount + 1) * self.m_weaponGachaMoneyCount
    local nextStepCount = math.ceil((nextBiggerWeaponNum - afterCurrSelectWeaponNum) / weaponNumStep)
    local nextNumberSelectorNumThreshold = nextStepCount + selectorNum






    if selectorNum < firstNumberSelectorNumThreshold then
        self.view.numWeaponNode.minButton.gameObject:SetActive(false)
        self.view.numWeaponNode.maxButton.gameObject:SetActive(true)
        self.m_weaponDecreaseNumber = selectorNum - 1
        self.m_weaponIncreaseNumber = firstNumberSelectorNumThreshold - selectorNum
    elseif selectorNum == firstNumberSelectorNumThreshold then
        self.view.numWeaponNode.minButton.gameObject:SetActive(true)
        self.view.numWeaponNode.maxButton.gameObject:SetActive(true)
        self.m_weaponDecreaseNumber = selectorNum - 1
        self.m_weaponIncreaseNumber = nextStepCount
    else
        self.view.numWeaponNode.minButton.gameObject:SetActive(true)
        self.view.numWeaponNode.maxButton.gameObject:SetActive(true)

        local afterDecreaseOneWeaponNum = afterCurrSelectWeaponNum - weaponNumStep
        local afterDecreaseOneGachaCount = math.floor(afterDecreaseOneWeaponNum / self.m_weaponGachaMoneyCount)
        local isPerfect = afterDecreaseOneGachaCount < currSelectWeaponGachaCount
        if isPerfect then

            local prevSmallerWeaponNum = (currSelectWeaponGachaCount - 1) * self.m_weaponGachaMoneyCount
            local prevStepCount = math.floor((afterCurrSelectWeaponNum - prevSmallerWeaponNum) / weaponNumStep)
            self.m_weaponDecreaseNumber = prevStepCount
        else

            local currGachaWeaponNum = currSelectWeaponGachaCount * self.m_weaponGachaMoneyCount
            local diff = afterCurrSelectWeaponNum - currGachaWeaponNum
            local currStepCount = math.floor(diff / weaponNumStep)
            self.m_weaponDecreaseNumber = currStepCount
        end
        self.m_weaponIncreaseNumber = nextStepCount
    end
    self.view.numWeaponNode.minButtonText.text = "-" .. self.m_weaponDecreaseNumber
    self.view.numWeaponNode.maxButtonText.text = "+" .. self.m_weaponIncreaseNumber
    if math.floor((selectorNum + self.m_weaponIncreaseNumber) * config.sourceMoneyMinSwap) > sourceNum then
        self.view.numWeaponNode.maxButtonStateController:SetState("MaxRed")
    else
        self.view.numWeaponNode.maxButtonStateController:SetState("Normal")
    end


    if sourceNum < config.sourceMoneyMinSwap then
        self.view.confirmTxt2.gameObject:SetActive(false)
    else
        self.view.confirmTxt2.gameObject:SetActive(true)
        local rightText = string.format(Language.LUA_COMMON_MONEY_EXCHANGE_WEAPON_CONFIRM_RIGHT, afterCurrSelectWeaponNum, currSelectWeaponGachaCount)
        self.view.confirmTxt2.text = rightText
    end
end

CommonMoneyExchangeCtrl._OnClickWeaponDecreaseBtn = HL.Method() << function(self)
    local curNum = self.view.numberSelector.curNumber
    local targetNum = curNum - self.m_weaponDecreaseNumber
    self.view.numberSelector:RefreshNumber(targetNum)
end

CommonMoneyExchangeCtrl._OnClickWeaponIncreaseBtn = HL.Method() << function(self)
    local sourceId = self.m_arg.sourceId
    local targetId = self.m_arg.targetId
    local existCfg, config = CS.Beyond.Gameplay.ShopSystem.GetExchangeData(sourceId, targetId)
    if not existCfg then
        logger.error("can not find money exchange data")
        return
    end
    local curNum = self.view.numberSelector.curNumber
    local targetNum = curNum + self.m_weaponIncreaseNumber

    local sourceNum = Utils.getItemCount(sourceId)
    if math.floor(targetNum * config.sourceMoneyMinSwap) > sourceNum then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_COMMON_MONEY_EXCHANGE_NEXT_WEAPON_GACHA_NOT_ENOUGH)
        return
    end
    self.view.numberSelector:RefreshNumber(targetNum)
end

CommonMoneyExchangeCtrl._OnGoToRechargeBtnClick = HL.Method() << function(self)
    PhaseManager:ExitPhaseFast(PHASE_ID)
    CashShopUtils.GotoCashShopRechargeTab()
end






CommonMoneyExchangeCtrl.OnClose = HL.Override() << function(self)
    if self.m_arg ~= nil and self.m_arg.onClose ~= nil then
        self.m_arg.onClose()
    end
end

CommonMoneyExchangeCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    if self.m_arg ~= nil then
        self.m_arg.onClose = nil
        self.m_arg.initExchangeCount = self.view.numberSelector.curNumber
    end
    return self.m_arg
end




HL.Commit(CommonMoneyExchangeCtrl)
